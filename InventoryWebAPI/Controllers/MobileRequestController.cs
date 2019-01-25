using InventoryBusinessLogic;
using InventoryBusinessLogic.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace InventoryWebAPI.Controllers
{
    public class MobileRequestController : ApiController
    {
        ManageRequestBusinessLogic request = new ManageRequestBusinessLogic();
        // GET: api/MobileRequest
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET: api/MobileRequest/5
        [HttpGet]
        [Route("api/MobileRequest/{userid}")]
        public IEnumerable<Request> GetRequestByOrderId(string deptid)
        {
              return request.GetRequestByOrderId(deptid);
        }

        // POST: api/MobileRequest
        public void Post([FromBody]string value)
        {
        }

        // PUT: api/MobileRequest/5
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE: api/MobileRequest/5
        public void Delete(int id)
        {
        }
    }
}
