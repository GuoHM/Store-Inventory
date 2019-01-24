using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using InventoryBusinessLogic.Entity;
using InventoryBusinessLogic;
using Microsoft.AspNet.Identity;

using System.Web.Script.Serialization;





namespace InventoryWeb.Controllers
{
    public class StoreSupervisorController : Controller
    {

        AdjustmentBusinessLogic req = new AdjustmentBusinessLogic();

        // GET: StoreSupervisor
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult ViewInventory()
        {
            return View();
        }

        public ActionResult ViewAdjustmentVoucher()
        {
            string userId = User.Identity.GetUserId();
            ViewBag.userID = userId;
            new AdjustmentBusinessLogic().getAllAdjustment(User.Identity.Name);
            return View();
        }


        public ActionResult ViewAdjustmentVoucherbyID()
        {
            string userId = User.Identity.GetUserId();
            new AdjustmentBusinessLogic().getAllAdjustment(userId);
            return View();
        }

        public ActionResult ViewAdjustmentVoucherItems(int adjustmentID)
        {

            new AdjustmentBusinessLogic().getAllAdjItems(adjustmentID);
            return View();

        }

      
        [HttpPost]
        public ActionResult SaveRequestStatus()
        {
            var sr = new System.IO.StreamReader(Request.InputStream);
            var stream = sr.ReadToEnd();
            JavaScriptSerializer js = new JavaScriptSerializer();
            var list = js.Deserialize<List<SelectedList>>(stream);

            if (list.Any())
            { 
                    new AdjustmentBusinessLogic().ApproveOrRejectRequest(list[0].AdjustmentID, list[0].requestStatus, list[0].remarks);
                    
            }
            return new JsonResult();
        }

        class SelectedList
        {
            public int AdjustmentID { get; set; }

            public string requestStatus { get; set; }
            public string remarks { get; set; }

        }

    }


}