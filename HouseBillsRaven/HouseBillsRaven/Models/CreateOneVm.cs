using System;

namespace HouseBillsRaven.Models
{
    public class CreateOneVm
    {
        public Guid PersonId { get; set; }
        public decimal Amount { get; set; }
    }
}