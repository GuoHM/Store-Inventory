using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Script.Serialization;
using Microsoft.AspNet.Identity;
using InventoryBusinessLogic;
using InventoryBusinessLogic.Entity;

namespace InventoryWeb.Controllers
{
    [Authorize(Roles = "DeptStaff")]
    public class StaffController : Controller
    {
        // GET: Staff
       public ActionResult RaiseRequest()
        {
            return View();
        }

        public ActionResult ViewRequest()
        {
            string userId = User.Identity.GetUserId();
            ViewBag.userID = userId;
            new ManageRequestBusinessLogic().getAllStationeryRequest(userId);
            return View();


        }

        public ActionResult ViewAllStationeryRequisitionsByOrderId(string orderId)
        {

            new ManageRequestBusinessLogic().getStationaryOrderByID(orderId);
            return View();
        }
    }
}