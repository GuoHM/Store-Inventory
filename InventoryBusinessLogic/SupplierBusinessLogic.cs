using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using InventoryBusinessLogic.Entity;

namespace InventoryBusinessLogic
{
    public class SupplierBusinessLogic
    {
        Inventory inventory = new Inventory();
        public Supplier FindSupplierById(string supplierId)
        {
            return inventory.Supplier.Where(x => x.SupplierID == supplierId).First();
        }
    }
}
