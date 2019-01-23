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

        public List<Order> depSpendings(DateTime date1,DateTime date2)
        {
            return inventory.Order.Where(i => i.OrderDate >= date1 && i.OrderDate <= date2).ToList<Order>();
        }





    }
}
