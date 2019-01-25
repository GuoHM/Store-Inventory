using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using InventoryBusinessLogic.Entity;

namespace InventoryBusinessLogic
{
    public class AdjustmentVoucherBusinessLogic
    {
        public Inventory inventory = new Inventory();

        public List<Adjustment> getAllAdjustment(string userid)
        {
            return inventory.Adjustment.Where(x => x.Supervisor == userid).ToList();
        }
        public List<AdjustmentList> getAllAdjItems(int adjustmentID)
        {
            List<AdjustmentItem> adjusItem = inventory.AdjustmentItem.Where(x => x.AdjustmentID == adjustmentID).ToList();
            List<AdjustmentList> adjustmentItem = new List<AdjustmentList>();
            foreach (AdjustmentItem req1 in adjusItem)
            {
                AdjustmentList ad = new AdjustmentList();
                ad.ItemID = req1.ItemID;
                ad.description = req1.Catalogue.Description;
                ad.quantity = req1.Quantity;
                ad.cost = req1.Catalogue.Price;
                ad.totalcost = Convert.ToDouble(req1.Quantity) * Convert.ToDouble(req1.Catalogue.Price);
                ad.reason = req1.Reason;
                adjustmentItem.Add(ad);

            }
            return adjustmentItem;
        }
        
    }
}
