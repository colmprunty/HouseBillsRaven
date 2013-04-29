using System;
using System.Collections.Generic;
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
        public List<Person> OtherUsers { get; protected set; } 

        protected override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            RavenSession = MvcApplication.DocumentStore.OpenSession();
            CurrentUser = (User == null || string.IsNullOrEmpty(User.Identity.Name) ? null : RavenSession.Load<Person>(new Guid(User.Identity.Name)));
            OtherUsers = (User == null || string.IsNullOrEmpty(User.Identity.Name) ? new List<Person>() : (from u in RavenSession.Query<Person>().Where(x => x.Name != CurrentUser.Name) select u).ToList());
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
