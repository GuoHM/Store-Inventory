using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using InventoryBusinessLogic.Entity;

namespace InventoryBusinessLogic
{
    public class CatalogueBusinessLogic
    {
        public Inventory inventory = new Inventory();
        public  List<Catalogue> getAllCatalogue()
        {
            return inventory.Catalogue.ToList();
        }

        public Catalogue getCatalogueByDescription(string description)
        {
            return inventory.Catalogue.Where(x => x.Description == description).First();
        }
        public Catalogue getCatalogueById(string catalogueId)
        {
            try
            {
                return inventory.Catalogue.Where(x => x.ItemID == catalogueId).First();
            } catch (Exception)
            {
                return null;
            }
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
      
        public void UpdateCataloguesByPurchaseID(int purchaseID)
        {
            List<PurchaseItem> purchaseItems = inventory.PurchaseItem.Where(x => x.PurchaseOrderID == purchaseID).ToList();
            foreach(PurchaseItem purchaseItem in purchaseItems)
            {
                purchaseItem.Catalogue.Quantity +=purchaseItem.Quantity;
                
            }
            PurchaseOrder purchaseOrder = inventory.PurchaseOrder.Where(x => x.PurchaseOrderID == purchaseID).First();
            purchaseOrder.PurchaseOrderStatus = "fullied";
            inventory.SaveChanges();
        }

        public List<Catalogue> GetLowStock()
        {
            return inventory.Catalogue.Where(x => x.Quantity <= x.ReorderLevel).ToList();
        }

       }
  
}
