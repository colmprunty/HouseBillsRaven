using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using HouseBillsRaven.Models;
using Raven.Client.Linq;

namespace HouseBillsRaven.Controllers
{
    public class DebtController : BaseController
    {
        public PartialViewResult CreateAll()
        {
            var model = new CreateAllVm();
            return PartialView(model);
        }

        [HttpPost]
        public ActionResult CreateDebtForAll(CreateAllVm createAll)
        {
            var count = OtherUsers.Count;
            foreach (var user in OtherUsers)
            {
                var debt = new Debt
                               {
                                   AddedDate = DateTime.Now,
                                   Amount = createAll.Amount/count,
                                   Description = createAll.Description,
                                   OwedBy = user,
                                   OwedTo = CurrentUser
                               };

                RavenSession.Store(debt);
            }

            RavenSession.SaveChanges();
            return RedirectToAction("Index", "Home");
        }

        public PartialViewResult GeneralList()
        {
            var model = new GeneralListVm();
            model.OwedToMe = (from d in RavenSession.Query<Debt>().Where(x => x.OwedTo.Id == CurrentUser.Id) select d).ToList();
            model.OwedToOthers = (from d in RavenSession.Query<Debt>().Where(x => x.OwedBy.Id == CurrentUser.Id) select d).ToList();
            return PartialView(model);
        }

        public PartialViewResult Breakdown()
        {
            var model = new List<BreakdownDto>();
            var allDebts = (from d in RavenSession.Query<Debt>() select d);

            foreach (var user in OtherUsers)
            {
                var iNeedToPay = allDebts.ToList().Where(x => x.OwedBy.Id == CurrentUser.Id && x.OwedTo.Id == user.Id && !x.Paid).Sum(y => y.Amount);
                var owedToMe = allDebts.ToList().Where(x => x.OwedTo.Id == CurrentUser.Id && x.OwedBy.Id == user.Id && !x.Paid).Sum(y => y.Amount);

                model.Add(new BreakdownDto
                              {
                                  Person = user, Amount = iNeedToPay - owedToMe
                              });
            }

            return PartialView(model);
        }

        public ActionResult ConsolidateDebts(Guid debtorId)
        {

            return RedirectToAction("Index", "Home");
        }
    }
}
