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
    }
}
