using System;
using System.Web.Mvc;
using HouseBillsRaven.Models;
using Raven.Client;

namespace HouseBillsRaven.Controllers
{
    public abstract class BaseController : Controller
    {
        public IDocumentSession RavenSession { get; set; }
        public Person CurrentUser { get; protected set; }

        protected override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            RavenSession = MvcApplication.DocumentStore.OpenSession();
            CurrentUser = (User == null || string.IsNullOrEmpty(User.Identity.Name) ? null : RavenSession.Load<Person>(new Guid(User.Identity.Name)));
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
