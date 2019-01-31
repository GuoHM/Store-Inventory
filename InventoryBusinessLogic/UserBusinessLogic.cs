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
            AspNetUsers user1 = inventory.AspNetUsers.Where(x => x.Id == userID).First<AspNetUsers>();
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
        public AspNetUsers getUserByUsername(string username)
        {
            return inventory.AspNetUsers.Where(x => x.UserName == username).First();
        }
        public List<AspNetUsers> getDepUsers(string userID)
        {
            AspNetUsers user1 = inventory.AspNetUsers.Where(x => x.Id == userID).First<AspNetUsers>();
            return inventory.AspNetUsers.Where(x => x.DepartmentID == user1.DepartmentID).ToList<AspNetUsers>();
        }

        public void UpdateDepRep(string id)
        {
            AspNetUsers user1 = inventory.AspNetUsers.Where(P => P.Id == id).First<AspNetUsers>();
            AspNetUsers user2 = inventory.AspNetUsers.Where(P => P.UserType == "DeptRep" && P.DepartmentID.Substring(0,4) == user1.DepartmentID.Substring(0, 4)).First<AspNetUsers>();
            AspNetUserRoles role1 = inventory.AspNetUserRoles.Where(p => p.UserId == user1.Id).First();
            AspNetUserRoles role2 = inventory.AspNetUserRoles.Where(p => p.UserId == user2.Id).First();
            Department dep1 = inventory.Department.Where(p => p.DepartmentID == user1.DepartmentID).First();
            user1.UserType = "DeptRep";
            user2.UserType = "DeptStaff";
            dep1.DepartmentRep = user1.Id;
     
           
            inventory.AspNetUserRoles.Remove(role1);
            inventory.AspNetUserRoles.Remove(role2);

            

            AspNetUserRoles userrole = new AspNetUserRoles();
            userrole.UserId = role1.UserId;
            userrole.RoleId = "2";
            inventory.AspNetUserRoles.Add(userrole);

            AspNetUserRoles userrole1 = new AspNetUserRoles();
            userrole1.UserId = role2.UserId;
            userrole1.RoleId = "4";
            inventory.AspNetUserRoles.Add(userrole1);

            inventory.SaveChanges();

        }

        public void UpdateDepHead(string id, DateTime startdate, DateTime enddate)
        {

            AspNetUsers user1 = inventory.AspNetUsers.Where(P => P.Id == id).First<AspNetUsers>();
            AspNetUsers user2 = inventory.AspNetUsers.Where(P => P.UserType == "DeptHead" && P.DepartmentID.Substring(0,4) == user1.DepartmentID.Substring(0,4)).First<AspNetUsers>();
            AspNetUserRoles role1 = inventory.AspNetUserRoles.Where(p => p.UserId == user1.Id).First();
            AspNetUserRoles role2 = inventory.AspNetUserRoles.Where(p => p.UserId == user2.Id).First();
            Department dep1 = inventory.Department.Where(p => p.DepartmentID == user1.DepartmentID).First();

            user1.UserType = "DeptHead";
            user2.UserType = "DeptStaff";
            dep1.DepartmentHead = user1.Id;


            inventory.AspNetUserRoles.Remove(role1);
            inventory.AspNetUserRoles.Remove(role2);



            AspNetUserRoles userrole = new AspNetUserRoles();
            userrole.UserId = role1.UserId;
            userrole.RoleId = "3";
            inventory.AspNetUserRoles.Add(userrole);

            AspNetUserRoles userrole1 = new AspNetUserRoles();
            userrole1.UserId = role2.UserId;
            userrole1.RoleId = "4";
            inventory.AspNetUserRoles.Add(userrole1);

            inventory.SaveChanges();

        }

        public List<Order> getDepSpendingHistory(DateTime date1,DateTime date2, string id)
        {
            AspNetUsers user1 = inventory.AspNetUsers.Where(x => x.Id == id).First<AspNetUsers>();
            return inventory.Order.Where(x => x.DepartmentID.Substring(0,4)==user1.DepartmentID.Substring(0,4) && x.OrderDate >= date1 && x.OrderDate<= date2 ).ToList<Order>();
        }

        public List<Order> getOverallSpendingHistory(DateTime date1, DateTime date2, string id)
        {
            Department dep = inventory.Department.Where(x => x.DepartmentID.Substring(0,4) == id.Substring(0,4)).First<Department>();
            return inventory.Order.Where(x => x.DepartmentID.Substring(0, 4) == dep.DepartmentID.Substring(0, 4) && x.OrderDate >= date1 && x.OrderDate <= date2).OrderBy(x => x.OrderDate).ToList<Order>();
        }

        public List<Catalogue> getAllCatalogue()
        {
            return inventory.Catalogue.ToList<Catalogue>();
        }

        public List<Request> getRequestOrders(DateTime date1,DateTime date2,string depID,string dropdown1)

        {


            Catalogue item = inventory.Catalogue.Where(x => x.ItemID == dropdown1).First<Catalogue>();
            return inventory.Request.Where(x => x.ItemID.Equals(item.ItemID) &&  x.RequestDate >= date1 && x.RequestDate <= date2 && x.RequestStatus == "Approved" && x.OrderID.Substring(0,4) == depID.Substring(0,4)).OrderBy(x => x.RequestDate).ToList<Request>();
        }

     
        public AspNetUsers getStoreStoreSupervisor()
        {
            return inventory.AspNetUsers.Where(x => x.UserType == "StoreSupervisor").First();
        }

        public AspNetUsers getStoreManager()
        {
            return inventory.AspNetUsers.Where(x => x.UserType == "Store Manager").First();
        }

        public List<Request> getPendigRequest(string id)
        {
            AspNetUsers user1 = inventory.AspNetUsers.Where(x => x.Id == id).First<AspNetUsers>();
            return inventory.Request.Where(x => x.RequestStatus == "Unapproved" && x.OrderID.Substring(0, 4) == user1.DepartmentID.Substring(0,4)).ToList<Request>();
        }

        public List<Request> getApproveorRejected(string id)
        {
            AspNetUsers user1 = inventory.AspNetUsers.Where(x => x.Id == id).First<AspNetUsers>();
            return inventory.Request.Where(x => (x.RequestStatus == "Rejected" || x.RequestStatus == "Approved") && x.OrderID.Substring(0, 4) == user1.DepartmentID.Substring(0,4)).ToList<Request>();
        }

    }
}
