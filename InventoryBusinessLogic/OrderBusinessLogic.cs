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
            departmentID = departmentID.Replace("\r\n", "");
            result += departmentID + GetWeekOfYear().Trim() + DateTime.Now.Year;
          //  result = result.Replace(@"\r\n", "");
            return result;
        }

        private string GetWeekOfYear()
        {
            //1. find the last day in firstweek of this year
            int firstWeekend = 7 - Convert.ToInt32(DateTime.Parse(DateTime.Today.Year + "-1-1").DayOfWeek);

            //2. get today is which days of this year
            int currentDay = DateTime.Today.DayOfYear;

            //3. (today - the first weekend day)/7
            int code =  Convert.ToInt32(Math.Ceiling((currentDay - firstWeekend) / 7.0)) + 1;
            if (code < 10)
            {
                return "0" + code;
            }
            return ""+code;
        }

        public Order GetOrderByOrderId(string orderid)
        {
            try
            {
                return inventory.Order.Where(x => x.OrderID == orderid).First();
            } catch (Exception)
            {
                return null;
            }         
        }

        public List<Order> GetAllOrders()
        {

            // return inventory.Order.Where(x=>x.Request.Any(y=>y.RequestStatus.Trim().ToUpper()=="APPROVED")).ToList();
            // return inventory.Order.Where(x => x.OrderStatus.ToUpper().Trim() == "APPROVED").ToList();
           var reqs = inventory.Request.Where(x => x.RequestStatus.ToUpper().Trim() == "APPROVED").Select(x => x.OrderID).Distinct().ToList();
            List<Order> orders = new List<Order>();
            foreach(var req in reqs)
            {
                Order order = inventory.Order.Where(x => x.OrderID == req).First();
                orders.Add(order);
            }
            return orders;
        }

        public List<Order> GetOrdersByStatus(string OrderStatus)
        {
            return inventory.Order.Where(x => x.OrderStatus == OrderStatus).ToList();
        }

        public void addOrder(Order order)
        {
            inventory.Order.Add(order);
            inventory.SaveChanges();
        }

        public void updateOrder(Order order)
        {
            Order update = inventory.Order.Where(x => x.OrderID == order.OrderID).First();
            update.OrderDate = order.OrderDate;
            update.OrderStatus = order.OrderStatus;
            update.TotalPrice = order.TotalPrice;
            update.Signature = order.Signature;
            update.DepartmentID = order.DepartmentID;
            inventory.SaveChanges();
        }

        public void updateSignture(string orderid,byte[] bt)
        {
            Order o = inventory.Order.Where( x => x.OrderID == orderid ).First();
            o.Signature = bt;
            o.OrderStatus = "Fulfilled";
            inventory.SaveChanges();
        }

        public byte[] getSignature(string orderid)
        {
            Order o = inventory.Order.Where(x => x.OrderID == orderid).First();
            return o.Signature;

        }

    }
}
