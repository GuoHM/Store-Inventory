using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using InventoryBusinessLogic.Entity;

namespace InventoryBusinessLogic
{
    
    public class UserBusinessLogic
    {
        Inventory inventory = new Inventory();

        public List<AspNetUsers> appointNewDepHead(string DepId = "1001")
        {
            return inventory.AspNetUsers.Where(x => x.DepartmentID == DepId).ToList<AspNetUsers>();
        }


        public List<AspNetUsers> getAllUser()
        {
            return inventory.AspNetUsers.ToList();
        }

        public AspNetUsers getUserByID(string ID)
        {
            return inventory.AspNetUsers.Where(x => x.Id == ID).First();
        }

        public List<AspNetUsers> getDepUsers(string DepId = "1001")
        {
            return inventory.AspNetUsers.Where(x => x.DepartmentID == DepId).ToList<AspNetUsers>();
        }

        public void UpdateDepRep(string id)
        {
            AspNetUsers user1 = inventory.AspNetUsers.Where(P => P.Id == id).First<AspNetUsers>();
            AspNetUsers user2 = inventory.AspNetUsers.Where(P => P.UserType == "DeptRep").First<AspNetUsers>();
            user1.UserType = "DeptRep";
            user2.UserType = "DeptStaff";
            inventory.SaveChanges();

        }

        public void UpdateDepHead(string id, DateTime startdate, DateTime enddate)
        {

            AspNetUsers user1 = inventory.AspNetUsers.Where(P => P.Id == id).First<AspNetUsers>();
            AspNetUsers user2 = inventory.AspNetUsers.Where(P => P.UserType == "DeptHead").First<AspNetUsers>();
            Department dep1 = inventory.Department.Where(P => P.DepartmentID == user2.DepartmentID).First<Department>();
            user1.UserType = "DeptHead";
            user2.UserType = "DeptStaff";
            dep1.DepartmentHeadStartDate = startdate;
            dep1.DepartmentHeadEndDate = enddate;
            inventory.SaveChanges();

        }

        public List<Order> getDepSpendingHistory(DateTime date1,DateTime date2,string DepId = "1001")
        {
            return inventory.Order.Where(x => x.DepartmentID==DepId && x.OrderDate >= date1 && x.OrderDate<= date2 ).ToList<Order>();
        }

        public List<Catalogue> getAllCatalogue()
        {
            return inventory.Catalogue.ToList<Catalogue>();
        }

        public List<Request> getRequestOrders(string dropdown1,DateTime date1,DateTime date2)

        {
            Catalogue item = inventory.Catalogue.Where(x => x.ItemID == dropdown1).First<Catalogue>();
             return inventory.Request.Where(x => x.ItemID.Equals(item.ItemID) && x.RequestDate >= date1 && x.RequestDate <= date2).ToList<Request>();
        }

     
    }
}
