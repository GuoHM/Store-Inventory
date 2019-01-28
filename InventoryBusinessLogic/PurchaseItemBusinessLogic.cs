using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using InventoryBusinessLogic.Entity;

namespace InventoryBusinessLogic
{
    public class PurchaseItemBusinessLogic
    {
        Inventory inventory = new Inventory();

        public void addPurchaseItem(PurchaseItem purchaseItem)
        {
            inventory.PurchaseItem.Add(purchaseItem);
            inventory.SaveChanges();
        }
        public List<PurchaseItem> getItemsByPurchaseOrderID(int OrderID)
        {

            return inventory.PurchaseItem.Where(x => x.PurchaseOrderID == OrderID).ToList();
        }

        public void getPOByID(int pod)
        {
           // int purchaseid = Po.PurchaseOrderID;

            var porder = inventory.PurchaseOrder.Where(x => x.PurchaseOrderID == pod).FirstOrDefault();

            if (porder.PurchaseOrderStatus.Trim() == "Unfullfill")
            {
                porder.PurchaseOrderStatus = "Cancel";
                inventory.SaveChanges();
            }


        }

    }

}
