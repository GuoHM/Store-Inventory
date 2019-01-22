using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using InventoryBusinessLogic.Entity;
using InventoryBusinessLogic;

namespace InventoryBusinessLogic
{
    public class AdjustmentItemBusinessLogic
    {
        Inventory inventory = new Inventory();
        public void addAdjustmentItem(AdjustmentItem adjustmentItem)
        {
            inventory.AdjustmentItem.Add(adjustmentItem);
            inventory.SaveChanges();
        }
    }
}
