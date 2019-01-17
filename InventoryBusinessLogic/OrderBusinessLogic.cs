using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using InventoryBusinessLogic.Entity;

namespace InventoryBusinessLogic
{
    public class OrderBusinessLogic
    {
        Inventory inventory = new Inventory();

        /// <summary>
        /// generate order id by current login user department and current week
        /// </summary>
        /// <param name="id">current login user id</param>
        /// <returns></returns>
        public string generateOrderIDById(string id)
        {
            string result="";
            string departmentID = inventory.AspNetUsers.Where(x => x.Id == id).First().DepartmentID;
            result += departmentID + GetWeekOfYear();
            return result;
        }

        private int GetWeekOfYear()
        {
            //1. find the last day in firstweek of this year
            int firstWeekend = 7 - Convert.ToInt32(DateTime.Parse(DateTime.Today.Year + "-1-1").DayOfWeek);

            //2. get today is which days of this year
            int currentDay = DateTime.Today.DayOfYear;

            //3. (today - the first weekend day)/7
            return Convert.ToInt32(Math.Ceiling((currentDay - firstWeekend) / 7.0)) + 1;
        }

        public Order GetOrderByOrderId(string orderid)
        {
            return inventory.Order.Where(x => x.OrderID == orderid).First();          
        }

        public void addOrder(Order order)
        {
            inventory.Order.Add(order);
            inventory.SaveChanges();
        }


    }
}
