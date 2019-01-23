using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using InventoryBusinessLogic.Entity;
using InventoryBusinessLogic;


namespace InventoryWeb.Controllers
{
    public class StoreSupervisorController : Controller
    {
        // GET: StoreSupervisor
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult ViewInventory()
        {
            return View();
        }




        public ActionResult Save(string itemid, int reorderlevel, int reorderquantity, int price)
        {
            new CatalogueBusinessLogic().Save(itemid, reorderlevel, reorderquantity, price);
            return View("ViewInventory");
        }



    }
}