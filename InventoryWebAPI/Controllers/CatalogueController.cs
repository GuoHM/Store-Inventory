﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace InventoryWebAPI.Controllers
{
    public class CatalogueController : ApiController
    {
        // GET: api/Catalogue
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
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
