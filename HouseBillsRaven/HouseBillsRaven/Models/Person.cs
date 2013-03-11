using System;

namespace HouseBillsRaven.Models
{
    public class Person
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public bool Alive { get; set; }
    }

}