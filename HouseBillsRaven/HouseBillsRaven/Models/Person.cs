using System;

namespace HouseBillsRaven.Models
{
    public class Person
    {
        public Guid Id { get; set; }
        public Guid InstanceId { get; set; }
        public string Name { get; set; }
        public bool Alive { get; set; }
        public bool Admin { get; set; }
        
        public void UpdateAdminStuff(bool alive, bool admin)
        {
            Admin = admin;
            Alive = alive;
        }
    }



}