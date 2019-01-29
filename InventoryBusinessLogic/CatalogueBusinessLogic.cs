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
      
        public Boolean UpdateCataloguesByPurchaseID(int purchaseID)
        {
            PurchaseOrder purchaseOrder = inventory.PurchaseOrder.Where(x => x.PurchaseOrderID == purchaseID).First();
            if (purchaseOrder.PurchaseOrderStatus.Trim().Equals("Unfulfill")) 
            {
                List<PurchaseItem> purchaseItems = inventory.PurchaseItem.Where(x => x.PurchaseOrderID == purchaseID).ToList();
                foreach (PurchaseItem purchaseItem in purchaseItems)
                {
                    purchaseItem.Catalogue.Quantity += purchaseItem.Quantity;

                }

                purchaseOrder.PurchaseOrderStatus = "Fulfilled";
                inventory.SaveChanges();
                return true;
            }

            return false;
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
        

        public void UpdateRetrievedQuantity(string itemDescription, string quantityPicked, string remarks)
        {
            var requests = inventory.Request.Where(x => x.Catalogue.Description == itemDescription).OrderBy(y=>y.RequestDate).ToList();
            if (quantityPicked != null && quantityPicked!="")
            {
                int quantity = Convert.ToInt32(quantityPicked);
                while (quantity != 0)
                {
                    foreach (Request req in requests)
                    {
                        int tempActual = Convert.ToInt32(req.Actual);
                        if (req.Needed != req.Actual)
                        {
                            if (quantity >= (req.Needed - tempActual))
                            {
                                req.Actual = req.Needed;
                                quantity = quantity - (Convert.ToInt32(req.Needed) - tempActual);
                            }
                            else
                            {
                                req.Actual = quantity + tempActual;
                                // quantity = quantity - (Convert.ToInt32(req.Actual) - tempActual);
                                quantity = 0;
                            }
                        }


                    }

                }
            }

            foreach(Request req in requests)
            {
                req.Remarks = remarks;
            }


            inventory.SaveChanges();
        }
        public void ValidateOrderStatus()
        {
            var distinctOrders = inventory.Request.Select(x => x.OrderID).Distinct().ToList();

            foreach (var order in distinctOrders)
            {
                var requests = inventory.Request.Where(x => x.OrderID == order).ToList();
                bool unfulfilled = false;
                foreach (var req in requests)
                {
                    if (req.Needed != req.Actual)
                    {
                        unfulfilled = true;
                    }
                }
                Order orderRecord = inventory.Order.Where(x => x.OrderID == order).First();

                if (unfulfilled)
                {

                    orderRecord.OrderStatus = "Unfulfilled";
                }
                else
                {
                    orderRecord.OrderStatus = "Fulfilled";

                }
                inventory.SaveChanges();

            }
        }





        }


    }



