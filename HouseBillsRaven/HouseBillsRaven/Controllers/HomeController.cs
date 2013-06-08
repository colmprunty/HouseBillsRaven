using System.Web.Mvc;

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
            return PartialView();
        }
    }
}
