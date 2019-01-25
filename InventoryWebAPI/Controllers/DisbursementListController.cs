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
    public class DisbursementListController : ApiController
    {
        DisbursementList dl = new DisbursementList();
        [HttpGet]
        [Route("api/Disbursement/{orderID}")]

        public List<Department> GetDisbursementByOrderID(string orderID)
        {
            return dl.GetDisbursements(orderID);
        }


    }
}
