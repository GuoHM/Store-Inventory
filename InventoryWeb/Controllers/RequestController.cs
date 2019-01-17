using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.IO;
using System.Web.Script.Serialization;
using InventoryBusinessLogic.Entity;
using InventoryBusinessLogic;
using Microsoft.AspNet.Identity;

namespace InventoryWeb.Controllers
{
    public class RequestController : Controller
    {
        // GET: Request
        [HttpPost]
        public ActionResult SaveRequest()
        {
            var sr = new StreamReader(Request.InputStream);
            var stream = sr.ReadToEnd();
            string username = User.Identity.GetUserId();
            JavaScriptSerializer js = new JavaScriptSerializer();
            var list = js.Deserialize<List<SelectedList>>(stream);
            CatalogueBusinessLogic catalogueBusinessLogic = new CatalogueBusinessLogic();
            if (list.Any())
            {
                foreach (var item in list)
                {
                    Catalogue catalogue = catalogueBusinessLogic.getCatalogueByDescription(item.description);

                }
            }
            return View();
        }
           
    }

    class SelectedList
    {
        public string description { get; set; }

        public string quantity { get; set; }
    }
}