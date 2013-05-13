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
        public ActionResult CreateDebtForOne(CreateOneVm createOne)
        {
            var owedBy = RavenSession.Load<Person>(createOne.PersonId);
            var debt = new Debt
                           {
                               AddedDate = DateTime.Now,
                               Amount = createOne.Amount,
                               Description = createOne.Description,
                               OwedBy = owedBy,
                               OwedTo = CurrentUser,
                               Paid = false
                           };

            RavenSession.Store(debt);
            return RedirectToAction("Index", "Home");
        }

        [HttpPost]
        public ActionResult CreateDebtForAll(CreateAllVm createAll)
        {
            var count = OtherUsers.Count+1;
            foreach (var user in OtherUsers)
            {
                var debt = new Debt
                               {
                                   AddedDate = DateTime.Now,
                                   Amount = createAll.Amount/count,
                                   Description = createAll.Description,
                                   OwedBy = user,
                                   OwedTo = CurrentUser,
                                   Paid = false
                               };

                RavenSession.Store(debt);
            }

            RavenSession.SaveChanges();
            return RedirectToAction("Index", "Home");
        }

        public PartialViewResult GeneralList()
        {
            var model = new GeneralListVm();
            model.OwedToMe = (from d in RavenSession.Query<Debt>().Where(x => x.OwedTo.Id == CurrentUser.Id && !x.Paid) select d).ToList();
            model.OwedToOthers = (from d in RavenSession.Query<Debt>().Where(x => x.OwedBy.Id == CurrentUser.Id && !x.Paid) select d).ToList();
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
                                  Person = user, Amount = owedToMe - iNeedToPay
                              });
            }

            return PartialView(model);
        }

        public ActionResult ConsolidateDebts(Guid debtorId)
        {
            var allDebts = (from d in RavenSession.Query<Debt>() select d);
            foreach (var debt in allDebts.Where(x => (x.OwedTo == CurrentUser && x.OwedBy.Id == debtorId) || (x.OwedTo.Id == debtorId && x.OwedBy == CurrentUser)))
            {
                debt.Paid = true;
            }

            return RedirectToAction("Index", "Home");
        }

        public ActionResult MarkAsPaid(Guid debtId)
        {
            var debt = RavenSession.Load<Debt>(debtId);
            debt.Paid = true;

            return RedirectToAction("Index", "Home");
        }

        public PartialViewResult CreateOne()
        {
            var people = (from p in RavenSession.Query<Person>().Where(x => x.Id != CurrentUser.Id) select p).ToList();
            var model = new CreateOneVm
                            {
                                People = (from p in people select new SelectListItem{ Text = p.Name, Value = p.Id.ToString()}).ToList()
                            };

            return PartialView(model);
        }
    }
}
