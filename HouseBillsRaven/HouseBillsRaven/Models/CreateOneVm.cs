using System;
using System.Collections.Generic;
using System.Web.Mvc;

namespace HouseBillsRaven.Models
{
    public class CreateOneVm
    {
        public Guid PersonId { get; set; }
        public decimal Amount { get; set; }
        public string Description { get; set; }
        public List<SelectListItem> People { get; set; }
    }
}