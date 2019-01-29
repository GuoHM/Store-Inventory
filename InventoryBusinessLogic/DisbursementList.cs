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

        public List<Order> GetDisbursementByDepartment(string userid)
        {
            var deptname = inv.AspNetUsers.Where(x => x.Id == userid).Select(x => x.DepartmentID).First();
            string depName = Convert.ToString(deptname);
            depName = depName.Replace("\r\n", "");
            return inv.Order.Where(x => x.DepartmentID.Replace("\r\n", "").Trim() == depName.Trim()).ToList();

        }

        public List<Request> GetDisburementItemsByDepartment(string orderID)
        {
            List<Request> requests = new List<Request>();
            List<Request> fulfilled = new List<Request>();
            requests = inv.Request.Where(x => x.OrderID == orderID).ToList();

            foreach(Request req in requests)
            {
                if(req.Actual>0)
                {
                    fulfilled.Add(req);
                }
            }
            return fulfilled;
        }

    }
}
