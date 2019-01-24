using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace InventoryWeb.Controllers
{
    public class StaffController : Controller
    {
        // GET: Staff
       public ActionResult RaiseRequest()
        {
            return View();
        }
    }
}