using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using InventoryBusinessLogic.Entity;

namespace InventoryBusinessLogic
{
     public class DepartmentBusinessLogic
    {
        Inventory inv = new Inventory();
        public List<Department> GetDepartments()
        {
            return inv.Department.ToList();
        }

        public List<Department> GetDepartmentByID(string id)
        {
            return inv.Department.Where(x => x.DepartmentID == id).ToList();
        }
    }
}
