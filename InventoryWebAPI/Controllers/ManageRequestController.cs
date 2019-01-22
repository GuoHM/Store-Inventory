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
    
    public class ManageRequestController : ApiController
    {
        
       
            ManageRequestBusinessLogic request = new ManageRequestBusinessLogic();
            [HttpGet]
            [Route("api/Request")]
            [AllowAnonymous]
            public IEnumerable<Request> GetAllRequests()
            {
                return request.GetAllRequests();
            }
            //GET: api/Catalogue/5
            [HttpGet]
            [Route("api/Request/{id}")]
            public Request GetRequestById(int id)
            {
                return request.GetRequestById(id);
            }


    }
}
