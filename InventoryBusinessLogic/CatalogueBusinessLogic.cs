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
            inventory.Configuration.ProxyCreationEnabled = false;
            return inventory.Catalogue.Where(x => x.Quantity <= x.ReorderLevel).ToList();
        }
        public void UpdateInventory(string id, string editBinId)
        {
            Catalogue item1 = inventory.Catalogue.Where(P => P.ItemID == id).First<Catalogue>();
            item1.BinNumber = editBinId;
            inventory.SaveChanges();

        }

        public void Save(string id, int reorderlevel, int reorderquantity, int price)
        {

            Catalogue item1 = inventory.Catalogue.Where(P => P.ItemID == id).First<Catalogue>();
            item1.ReorderLevel = reorderlevel;
            item1.ReorderQuantity = reorderquantity;
            item1.Price = price;

            inventory.SaveChanges();


        }

        public List<Order> depSpendings(DateTime date1, DateTime date2)
        {
            return inventory.Order.Where(i => i.OrderDate >= date1 && i.OrderDate <= date2).ToList<Order>();
        }





    }


}



