using System.Web.Mvc;
using HouseBillsRaven.Models;

namespace HouseBillsRaven.Controllers
{
    public class PersonController : BaseController
    {
        [HttpGet]
        public ActionResult InsertPerson()
        {
            return View();
        }

        [HttpPost]
        public ActionResult InsertPerson(Person newPerson)
        {
            RavenSession.Store(newPerson);
            RavenSession.SaveChanges();
            return RedirectToAction("Index", "Home");
        }

    }
}
