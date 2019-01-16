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
        public static Inventory inventory = new Inventory();
        public static List<Catalogue> getAllCatalogue()
        {
            return inventory.Catalogue.ToList();
        }
    }
}
