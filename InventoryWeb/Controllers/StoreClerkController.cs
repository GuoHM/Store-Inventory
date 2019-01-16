using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace InventoryWeb.Controllers
{
    [Authorize(Roles ="StoreClerk")]
    public class StoreClerkController : Controller
    {
   
        public ActionResult RaiseRequest()
        {
            return View();
        }
    }
}