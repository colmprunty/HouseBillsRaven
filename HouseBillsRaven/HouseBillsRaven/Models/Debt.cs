using System;

namespace HouseBillsRaven.Models
{
    public class Debt
    {
        public Guid Id { get; set; }
        public DateTime AddedDate { get; set; }
        public string Description { get; set; }
        public decimal Amount { get; set; }
        public Person OwedBy { get; set; }
        public Person OwedTo { get; set; }
    }
}