using System.Linq;
using System.Web.Mvc;
using HouseBillsRaven.Models;
using Raven.Client;
using Raven.Client.Linq;

namespace HouseBillsRaven.Controllers
{
    public abstract class BaseController : Controller
    {
        public IDocumentSession RavenSession { get; set; }
        public Person CurrentUser { get; protected set; }

        protected override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            RavenSession = MvcApplication.DocumentStore.OpenSession();
            CurrentUser = User == null ? null : (from p in RavenSession.Query<Person>() where p.Name == User.Identity.Name select p).SingleOrDefault();
        }

        protected override void OnActionExecuted(ActionExecutedContext filterContext)
        {
            if (filterContext.IsChildAction)
                return;

            using (RavenSession)
            {
                if (filterContext.Exception != null)
                    return;

                if (RavenSession != null)
                    RavenSession.SaveChanges();
            }
        }
    }
}
