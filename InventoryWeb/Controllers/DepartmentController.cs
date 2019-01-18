using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using InventoryBusinessLogic;
using InventoryBusinessLogic.Entity;

namespace InventoryWeb.Controllers
{
    public class DepartmentController : Controller
    {
        DepartmentBusinessLogic obj = new DepartmentBusinessLogic();
        // GET: Department
        public ActionResult show()
        {
            
            return View();

        }
    }
}