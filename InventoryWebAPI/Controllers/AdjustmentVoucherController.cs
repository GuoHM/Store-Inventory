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
    public class AdjustmentVoucherController : ApiController
    {
        AdjustmentVoucherBusinessLogic adjustment = new AdjustmentVoucherBusinessLogic();
        [HttpGet]
        [Route("api/Adjustment/{UserID}")]

        public IEnumerable<Adjustment> GetAllAdjustments(string UserID)
        {
            return adjustment.getAllAdjustment(UserID);
        }
        [HttpGet]
        [Route("api/AdjustmentItem/{adjustmentID}")]
        public IEnumerable<AdjustmentList> GetAllAdjustmentItems(int adjustmentID)
        {
            return adjustment.getAllAdjItems(adjustmentID);
        }


    }
}
