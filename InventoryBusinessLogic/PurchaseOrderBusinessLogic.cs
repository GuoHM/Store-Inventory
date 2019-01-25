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
            try
            {
                return inventory.PurchaseOrder.Max(x => x.PurchaseOrderID) +1;
            }
            catch (Exception)
            {
                return 1000;
            }
        }

        public List<PurchaseOrder> GetAllPurchaseOrders()
        {
            return inventory.PurchaseOrder.ToList();
        }

        public List<PurchaseItem> GetAllPurchaseOrderById(int orderID)
        {
            return inventory.PurchaseItem.Where(x => x.PurchaseOrderID == orderID).ToList();
        }

        public void updatePurchaseOrder(PurchaseOrder purchaseOrder)
        {
            PurchaseOrder update = inventory.PurchaseOrder.Where(x => x.PurchaseOrderID == purchaseOrder.PurchaseOrderID).First();
            update.SupplierID = purchaseOrder.SupplierID;
            update.TotalPrice = purchaseOrder.TotalPrice;
            update.PurchaseDate = purchaseOrder.PurchaseDate;
            update.OrderBy = purchaseOrder.OrderBy;
            update.PurchaseOrderStatus = purchaseOrder.PurchaseOrderStatus;
            update.ExpectedDate = purchaseOrder.ExpectedDate;
            update.DeliverAddress = purchaseOrder.DeliverAddress;
            inventory.SaveChanges();
        }

        public PurchaseOrder findById(int id)
        {
            return inventory.PurchaseOrder.Where(x => x.PurchaseOrderID == id).First();
        }
    }
}
