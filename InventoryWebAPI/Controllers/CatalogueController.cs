using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using InventoryBusinessLogic;
using InventoryBusinessLogic.Entity;

namespace InventoryWebAPI.Controllers
{
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
