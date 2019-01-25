using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Cors;
using InventoryBusinessLogic;
using InventoryBusinessLogic.Entity;

namespace InventoryWebAPI.Controllers
{
    [EnableCors(origins: "*", headers: "*", methods: "*")]
    public class OrdersController : ApiController
    {
        OrderBusinessLogic orders = new OrderBusinessLogic();

        [HttpGet]
        [Route("api/Orders")]

        public IEnumerable<Order> GetAllOrders()
        {
            return orders.GetAllOrders();
        }

        [HttpGet]
        [Route("api/Orders/{OrderStatus}")]
        public IEnumerable<Order> GetRequestByOrderStatus(string status)
        {
            return orders.GetOrdersByStatus(status);
        }
    }
}
