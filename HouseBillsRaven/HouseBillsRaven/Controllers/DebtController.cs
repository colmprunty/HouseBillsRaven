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
            //var people = (from p in RavenSession.Query<Person>().Where(x => x.Alive).ToList() select new SelectListItem{ Value = p.Id.ToString(), Text = p.Name}).ToList();
            var model = new CreateAllVm();
            return PartialView(model);
        }

        [HttpPost]
        public ActionResult CreateDebtForAll(CreateAllVm createAll)
        {
            return RedirectToAction("Index", "Home");
        }
    }
}
