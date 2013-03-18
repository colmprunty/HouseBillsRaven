﻿using System;
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
            var otherUsers = (from u in RavenSession.Query<Person>().Where(x => x.Name != CurrentUser.Name) select u).ToList();
            var count = otherUsers.Count;
            foreach (var user in otherUsers)
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
    }
}
