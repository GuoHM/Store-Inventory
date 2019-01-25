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
    public class ManageRequestController : ApiController
    {
        
       
            ManageRequestBusinessLogic request = new ManageRequestBusinessLogic();
            [HttpGet]
            [Route("api/Request")]

            public IEnumerable<Request> GetAllRequests()
            {
                return request.GetAllRequests();
            }
            //GET: api/Catalogue/5
            //[HttpGet]
            //[Route("api/Request/{id}")]
            //public Request GetRequestById(int id)
            //{
            //    return request.GetRequestById(id);
            //}

        [HttpGet]
        [Route("api/Request/{OrderId}")]
        public IEnumerable<Request> GetRequestByOrderId(string OrderId)
        {
            return request.GetRequestByOrderId(OrderId);
        }

        [HttpGet]
        [Route("api/Adjustment/{Userid}")]
        public IEnumerable<Adjustment> GetAdjustmentByUserid(string Userid)
        {
            AdjustmentBusinessLogic adjustment = new AdjustmentBusinessLogic();
           return adjustment.getAllAdjustmentList(Userid).ToList();
        }

    }
}
