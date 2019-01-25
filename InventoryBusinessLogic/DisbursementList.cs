using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using InventoryBusinessLogic.Entity;
namespace InventoryBusinessLogic
{
   public class DisbursementList
    {
        Inventory inv = new Inventory();
        public List<Department> GetDisbursements(string orderID)
        {
           Order orders = inv.Order.Where(x => x.OrderID == orderID).First();
            return inv.Department.Where(x => x.DepartmentID == orders.DepartmentID).ToList();
        }

        public List<Request> GetDisbursementList(string deptName, string orderID)
        {
            return inv.Request.Where(x => x.Order.Department.DepartmentName == deptName && x.OrderID==orderID&& x.RequestStatus.ToUpper().Trim()=="APPROVED").ToList();
           
        }

    }
}
