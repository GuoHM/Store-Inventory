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
            return inventory.AspNetUsers.Where(x => x.DepartmentID == DepId && x.UserType == "Rep" || x.UserType == "Employee").ToList<AspNetUsers>();
        }

        public void UpdateDepRep(string id)
        {
            AspNetUsers user1 = inventory.AspNetUsers.Where(P => P.Id == id).First<AspNetUsers>();
            AspNetUsers user2 = inventory.AspNetUsers.Where(P => P.UserType == "Rep").First<AspNetUsers>();
            user2.UserType = "Employee";
            user1.UserType = "Rep";
            inventory.SaveChanges();

        }

        public void UpdateDepHead(string id, DateTime startdate, DateTime enddate)
        {

            AspNetUsers user1 = inventory.AspNetUsers.Where(P => P.Id == id).First<AspNetUsers>();
            AspNetUsers user2 = inventory.AspNetUsers.Where(P => P.UserType == "DeptHead").First<AspNetUsers>();
            Department dep1 = inventory.Department.Where(P => P.DepartmentID == user2.DepartmentID).First<Department>();
            user2.UserType = "Employee";
            user1.UserType = "De";
            dep1.DepartmentHeadStartDate = startdate;
            dep1.DepartmentHeadEndDate = enddate;
            inventory.SaveChanges();

        }
        public List<AspNetUsers> getAllstoreClerk()
        {
            return inventory.AspNetUsers.Where(x => x.UserType == "StoreClerk").ToList();
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
