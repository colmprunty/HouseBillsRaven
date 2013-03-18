using System.Collections.Generic;

namespace HouseBillsRaven.Models
{
    public class GeneralListVm
    {
        public GeneralListVm()
        {
            OwedToMe = new List<Debt>();
            OwedToOthers = new List<Debt>();
        }

        public List<Debt> OwedToOthers { get; set; }
        public List<Debt> OwedToMe { get; set; }
    }
}