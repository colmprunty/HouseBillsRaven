using System;
using System.Web.Mvc;
using HouseBillsRaven.Models;

namespace HouseBillsRaven.Controllers
{
    public class DebugController : BaseController
    {
        public ContentResult CreatePeople()
        {
            var people = RavenSession.Query<Person>();
            foreach (var person in people)
            {
                RavenSession.Delete(person);
            }

            var colm = new Person
                           {
                               Id = Guid.NewGuid(),
                               Name = "Colm",
                               Alive = true
                           };

            var weija = new Person
                            {
                                Id = Guid.NewGuid(),
                                Name = "Weija",
                                Alive = true
                            };

            RavenSession.Store(colm);
            RavenSession.Store(weija);
            RavenSession.SaveChanges();

            return Content("All done");
        }
    }
}
