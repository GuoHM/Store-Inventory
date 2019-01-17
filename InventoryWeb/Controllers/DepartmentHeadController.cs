using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace InventoryWeb.Controllers
{
    public class DepartmentHeadController : Controller
    {
        // GET: DepartmentHead
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult ApproveOrRejectRequest()
        {
            return View();
        }
    }
}