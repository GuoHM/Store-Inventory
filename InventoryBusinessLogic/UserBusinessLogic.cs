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

        public List<AspNetUsers> appointNewDepHead(string userID)
        {
            AspNetUsers user1 = inventory.AspNetUsers.Where(P => P.Id == userID).First<AspNetUsers>();
            return inventory.AspNetUsers.Where(x => x.DepartmentID == user1.DepartmentID).ToList<AspNetUsers>();
        }

        public List<AspNetUsers> getAllUser()
        {
            return inventory.AspNetUsers.ToList();
        }

        public AspNetUsers getUserByID(string ID)
        {
            return inventory.AspNetUsers.Where(x => x.Id == ID).First();
        }

        public List<AspNetUsers> getDepUsers(string userID)
        {
            AspNetUsers user1 = inventory.AspNetUsers.Where(P => P.Id == userID).First<AspNetUsers>();
            return inventory.AspNetUsers.Where(x => x.DepartmentID == user1.DepartmentID).ToList<AspNetUsers>();
        }

        public void UpdateDepRep(string id)
        {
            AspNetUsers user1 = inventory.AspNetUsers.Where(P => P.Id == id).First<AspNetUsers>();
            AspNetRoles user1role = inventory.AspNetRoles.Where(p => p.Name == user1.UserType).First<AspNetRoles>();
            AspNetUserRoles user1IdRole = inventory.AspNetUserRoles.Where(p => p.UserId == user1.Id && p.RoleId == user1role.Id).First<AspNetUserRoles>();

            AspNetUsers user2 = inventory.AspNetUsers.Where(P => P.UserType == "DeptRep" && P.DepartmentID == user1.DepartmentID).First<AspNetUsers>();
            AspNetRoles user2role = inventory.AspNetRoles.Where(p => p.Name == user2.UserType).First<AspNetRoles>();
            AspNetUserRoles user2IdRole = inventory.AspNetUserRoles.Where(p => p.UserId == user2.Id && p.RoleId == user2role.Id).First<AspNetUserRoles>();

            string temp = user1.UserType;
            user1.UserType = user2.UserType;
            user2.UserType = temp;


            string temp2 = user1IdRole.RoleId;
            user1IdRole.RoleId = user2IdRole.RoleId;
            user2IdRole.RoleId = temp2;
         
            
            inventory.sa

        }

        public void UpdateDepHead(string id, DateTime startdate, DateTime enddate)
        {

            AspNetUsers user1 = inventory.AspNetUsers.Where(P => P.Id == id).First<AspNetUsers>();
            AspNetUsers user2 = inventory.AspNetUsers.Where(P => P.UserType == "DeptHead").First<AspNetUsers>();
            Department dep1 = inventory.Department.Where(P => P.DepartmentID == user2.DepartmentID).First<Department>();
            string temp = user1.UserType;
            user1.UserType = user2.UserType;
            user2.UserType = temp;
            //AspNetUserRoles alpha = inventory.AspNetUserRoles.Where(p => p.UserId == user1.Id).First<AspNetUserRoles>();
            //AspNetUserRoles alpha2 = inventory.AspNetUserRoles.Where(p => p.UserId == user2.Id).First<AspNetUserRoles>();
           
           
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
             return inventory.Request.Where(x => x.ItemID.Equals(item.ItemID) &&  x.RequestDate >= date1 && x.RequestDate <= date2 && x.RequestStatus == "Approved" ).ToList<Request>();
        }

     
        public AspNetUsers getStoreStoreSupervisor()
        {
            return inventory.AspNetUsers.Where(x => x.UserType == "StoreSupervisor").First();
        }

        public AspNetUsers getStoreManager()
        {
            return inventory.AspNetUsers.Where(x => x.UserType == "Store Manager").First();
        }

    }
}
