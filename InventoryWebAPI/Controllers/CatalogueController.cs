using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using InventoryBusinessLogic;
using InventoryBusinessLogic.Entity;
using System.Web.Http.Cors;

namespace InventoryWebAPI.Controllers
{
    [EnableCors(origins: "*", headers: "*", methods: "*")]
    public class CatalogueController : ApiController
    {
        CatalogueBusinessLogic catalogue = new CatalogueBusinessLogic();
        // GET: api/Catalogue

        [HttpGet]
        [Route("api/Catalogue")]
      
        public IEnumerable<Catalogue> GetAllCatalogues()
        {
            return catalogue.getAllCatalogue();
        }

        [HttpGet]
        [Route("api/Lowstock")]
        public IEnumerable<Catalogue> GetLowStocks()
        {
            return catalogue.GetLowStock();
        }

        // GET: api/Catalogue/5
        public string Get(int id)
        {
            return "value";
        }

        // POST: api/Catalogue
        public void Post([FromBody]string value)
        {
        }

        // PUT: api/Catalogue/5
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE: api/Catalogue/5
        public void Delete(int id)
        {
        }
    }
}
