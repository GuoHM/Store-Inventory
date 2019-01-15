using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace InventoryWeb.Controllers
{
    public class ManageRequestController : Controller
    {
        public ActionResult RaiseRequest()
        {
            return View();
        }
    }
}