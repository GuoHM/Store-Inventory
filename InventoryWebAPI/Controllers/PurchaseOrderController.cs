using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using InventoryBusinessLogic.Entity;
using InventoryBusinessLogic;
using System.Web.Http.Cors;


namespace InventoryWebAPI.Controllers
{
    [EnableCors(origins: "*", headers: "*", methods: "*")]
    public class PurchaseOrderController : ApiController
    {
        PurchaseOrderBusinessLogic po = new PurchaseOrderBusinessLogic();
        
        [HttpGet]
        [Route("api/PurchaseOrder")]

        public IEnumerable<PurchaseOrder> GetAllPurchaseOrders()
        {
            return po.GetAllPurchaseOrders();
        }
        [HttpGet]
        [Route("api/PurchaseOrder/{OrderId}")]
        public List<PurchaseItem> GetPurchaseOrderById(int OrderId)
        {
            return po.GetAllPurchaseOrderById(OrderId);
        }
    }
}
