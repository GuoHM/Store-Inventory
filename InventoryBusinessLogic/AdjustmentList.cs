using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InventoryBusinessLogic
{
  public  class AdjustmentList
    {
        public string ItemID { get; set; }
        public string description { get; set; }
        public string quantity { get; set; }

        public double? cost { get; set; }

        public double totalcost { get; set; }
        public string reason { get; set; }
    }
}
