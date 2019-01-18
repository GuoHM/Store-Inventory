using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using InventoryBusinessLogic.Entity;

namespace InventoryBusinessLogic
{
    public class PurchaseOrderBusinessLogic
    {
        Inventory inventory = new Inventory();
        public void addPurchaseOrder(PurchaseOrder purchaseOrder)
        {
            inventory.PurchaseOrder.Add(purchaseOrder);
            inventory.SaveChanges();
        }

        public int generatePurchaseOrderID()
        {
            return inventory.PurchaseOrder.Max(x => x.PurchaseOrderID);
        }
    }
}
