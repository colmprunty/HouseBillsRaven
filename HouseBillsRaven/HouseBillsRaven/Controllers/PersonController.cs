using System.Linq;
using System.Web.Mvc;
using System.Web.Security;
using HouseBillsRaven.Models;
using Raven.Client.Linq;

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

        public ActionResult Login()
        {
            var model = new LoginVm();
            return View(model);
        }

        public ActionResult LoginUser(LoginVm model)
        {
            var user = RavenSession.Query<Person>().SingleOrDefault(x => x.Name == model.Name && x.Alive);
            if (user == null)
                return RedirectToAction("Login", new LoginVm());

            FormsAuthentication.SetAuthCookie(model.Name, false);
            return RedirectToAction("Index", "Home");
        }

        public ActionResult Logout()
        {
            FormsAuthentication.SignOut();
            return RedirectToAction("Login", new LoginVm());
        }
    }
}
