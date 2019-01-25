using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.IO;
using System.Web.Script.Serialization;
using InventoryBusinessLogic.Entity;
using InventoryBusinessLogic;
using Microsoft.AspNet.Identity;

namespace InventoryWeb.Controllers
{
    public class RequestController : Controller
    {
        CatalogueBusinessLogic catalogueBusinessLogic = new CatalogueBusinessLogic();
        OrderBusinessLogic orderBusinessLogic = new OrderBusinessLogic();
        UserBusinessLogic userBusinessLogic = new UserBusinessLogic();
        ManageRequestBusinessLogic manageRequestBusinessLogic = new ManageRequestBusinessLogic();

        // GET: Request
        [HttpPost]
        public ActionResult SaveRequest()
        {
            var sr = new StreamReader(Request.InputStream);
            var stream = sr.ReadToEnd();
            string username = User.Identity.GetUserId();
            JavaScriptSerializer js = new JavaScriptSerializer();
            var list = js.Deserialize<List<SelectedList>>(stream);
            if (list.Any())
            {
                foreach (var item in list)
                {
                    Catalogue catalogue = catalogueBusinessLogic.getCatalogueByDescription(item.description);
                    string orderid = orderBusinessLogic.generateOrderIDById(User.Identity.GetUserId());
                    Order order = orderBusinessLogic.GetOrderByOrderId(orderid);
                    if (order == null)
                    {
                        //order do not exist,insert
                        order = new Order();
                        order.OrderID = orderid;
                        order.DepartmentID = userBusinessLogic.getUserByID(User.Identity.GetUserId()).DepartmentID;
                        order.OrderDate = DateTime.Now;
                        order.TotalPrice = 0;
                        order.TotalPrice += Convert.ToInt32(item.quantity) * catalogue.Price;
                        order.OrderStatus = "Unfullfill";
                        orderBusinessLogic.addOrder(order);                        
                    } else
                    {
                        //order exist,update
                        order.TotalPrice += Convert.ToInt32(item.quantity) * catalogue.Price;
                        orderBusinessLogic.updateOrder(order);
                    }
                    Request request = manageRequestBusinessLogic.GetRequestsByOrderAndItem(orderid, catalogue.ItemID);
                    if (request == null || request.RequestStatus == "Reject")
                    {
                        //1.no exist item in request, insert one
                        //2.exist but reject before, than create new request
                        request = new Request();
                        request.Needed = Convert.ToInt32(item.quantity);
                        request.ItemID = catalogue.ItemID;
                        request.RequestDate = DateTime.Now;
                        request.UserID = User.Identity.GetUserId();
                        request.OrderID = orderid;
                        request.RequestStatus = "Unapproved";
                        request.Actual = 0;
                        manageRequestBusinessLogic.addRequest(request);

                    } else {
                        //request exist, update
                        request.Needed += Convert.ToInt32(item.quantity);
                        request.RequestDate = DateTime.Now;
                        manageRequestBusinessLogic.UpdateRequest(request);

                    }
                }
            }
            EmailBusinessLogic emailBusinessLogic = new EmailBusinessLogic();
            string content = emailBusinessLogic.SendRequestNotification(username);

            List<string> toAddress = new List<string>();
            toAddress.Add("wangxiaoxiaoqiang@gmail.com");
            emailBusinessLogic.SendEmail("Team3", content, toAddress);

            return new JsonResult();
        }
        class SelectedList
        {
            public string description { get; set; }

            public string quantity { get; set; }
            public string requestStatus { get; set; }
            public string orderId { get; set; }
            public string reason { get; set; }
        }

    }

    
}