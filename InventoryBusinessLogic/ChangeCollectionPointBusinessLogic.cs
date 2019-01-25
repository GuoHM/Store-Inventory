using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using InventoryBusinessLogic.Entity;

namespace InventoryBusinessLogic
{
    public class ChangeCollectionPointBusinessLogic
    {
        Inventory inventory = new Inventory();
        //Department department = new Department();
        public void ChangeCollectionPoint(string CollectionPoint,string userId)
        {
            AspNetUsers user = inventory.AspNetUsers.Where(x => x.UserName == userId).First();
            var request = inventory.Department.Where(x => x.DepartmentID == user.DepartmentID).First();
            request.CollectionPoint = CollectionPoint;
            inventory.SaveChanges();
        }

        public List<Department> getAllDepartment()
        {
            return inventory.Department.ToList();
        }

        public dynamic getDeptByID(object userId)
        {
            throw new NotImplementedException();
        }

        public Department getDeptByID(string UserId)
        {
            return inventory.Department.Where(x => x.AspNetUsers.UserName == UserId).First();

        }
        //public Department getDeptByID(string DepartmentId)
        //{
        //    return inventory.Department.Where(x => x.DepartmentID == DepartmentId).First();

        //}


    }
}