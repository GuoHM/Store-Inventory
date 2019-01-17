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
                    Request request = new Request();
                    request.Needed = Convert.ToInt32(item.quantity);
                    request.ItemID = catalogue.ItemID;
                    request.RequestDate = DateTime.Now;
                    request.UserID = User.Identity.GetUserId();
                    request.OrderID = orderBusinessLogic.generateOrderIDById(User.Identity.GetUserId());
                    Order order = orderBusinessLogic.GetOrderByOrderId(request.OrderID);
                    if (order == null)
                    {
                        //order do not exist,insert
                        order = new Order();
                        order.OrderID = request.OrderID;
                        order.DepartmentID = userBusinessLogic.getUserByID(User.Identity.GetUserId()).DepartmentID;
                        order.OrderDate = DateTime.Now;
                        order.TotalPrice = 0;
                        order.TotalPrice += request.Needed * catalogue.Price;
                        orderBusinessLogic.addOrder(order);                        
                    } else
                    {
                        order.TotalPrice += request.Needed * catalogue.Price;
                        orderBusinessLogic.updateOrder(order);
                    }
                    manageRequestBusinessLogic.addRequest(request);


                }
            }
            return new JsonResult();
        }
   

           
    }

    class SelectedList
    {
        public string description { get; set; }

        public string quantity { get; set; }
    }
}