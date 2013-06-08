using System.Linq;
using System.Web.Mvc;
using HouseBillsRaven.Models;

namespace HouseBillsRaven.Controllers
{
    public class HomeController : BaseController
    {
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Admin()
        {
            var model = new AdminVm();
            model.People = (from p in RavenSession.Query<Person>().Where(x => x.InstanceId == CurrentUser.InstanceId) orderby p.Name select p).OrderBy(x => x.Alive).ToList();
            return PartialView(model);
        }
    }
}
