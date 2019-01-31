using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using InventoryBusinessLogic.Entity;
using InventoryBusinessLogic;
using System.Web.Http.Cors;
using Newtonsoft.Json;

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
        [HttpGet]
        [Route("api/PendingRequest/{userid}")]

        public IEnumerable<Request> GetAllPendingRequests(string userid)
        {
            return request.GetAllApprovalPendingRequests(userid);
        }

        [HttpGet]
        [Route("api/PendingRequest/{Orderid}/{Userid}")]

        public IEnumerable<Request> GetAllPendingRequestsbyUserID(string Orderid, string Userid)
        {
            return request.GetRequestByOrderIdUserId(Orderid, Userid);
        }

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
        [Route("api/RequestItems/{userId}")]
        public IEnumerable<Request> GetItemsByUser(string userId)
        {
            return request.getAllStationeryRequest(userId);
        }

        [HttpGet]
        [Route("api/StationaryItems/{OrderId}/{UserId}/{Status}")]
        public IEnumerable<Request> GetRequestByOrder(string orderId,string userId,string status)
        {
            return request.getStationaryOrderByID(orderId, userId,status);
        }


    }
}
