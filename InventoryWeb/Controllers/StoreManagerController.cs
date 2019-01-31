using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using InventoryBusinessLogic.Entity;
using InventoryBusinessLogic;
using Microsoft.AspNet.Identity;
using System.Web.Script.Serialization;
using Newtonsoft.Json;

namespace InventoryWeb.Controllers
{
    [Authorize(Roles = "StoreManager")]
    public class StoreManagerController : Controller
    {
        // GET: StoreManager
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult ViewAdjustmentVoucherManager()
        {
            string userId = User.Identity.GetUserId();
            ViewBag.userID = userId;
            new AdjustmentBusinessLogic().getAllAdjustment(User.Identity.Name);
            return View();
        }

        public ActionResult ViewAdjustmentVoucherbyID()
        {
            string userId = User.Identity.GetUserId();
            ViewBag.userID = userId;
            new AdjustmentBusinessLogic().getAllAdjustment(userId);
            return View();
        }

        public ActionResult generateChargeBack()
        {
            return View();
        }

        public ActionResult ChargeBackReport(DateTime date1, DateTime date2)
        {
            CatalogueBusinessLogic bl = new CatalogueBusinessLogic();

            List<Department> dep = bl.getDepartments();
            ReportsController depManager = new ReportsController();


            foreach (Department d in dep)
            {
                depManager.spendingHistorytwo(date1, date2, d.DepartmentID);
            }

            ViewBag.dataSCI = JsonConvert.SerializeObject(depManager.dataSCI);
            ViewBag.dataCOMM = JsonConvert.SerializeObject(depManager.dataCOMM);
            ViewBag.dataCPSC = JsonConvert.SerializeObject(depManager.dataCPSC);
            ViewBag.dataENGL = JsonConvert.SerializeObject(depManager.dataENGL);
            ViewBag.dataREGR = JsonConvert.SerializeObject(depManager.dataREGR);
            ViewBag.dataZOOL = JsonConvert.SerializeObject(depManager.dataZOOL);
            ViewBag.months = JsonConvert.SerializeObject(depManager.datamonths);
            return View("generateChargeBack");




        }

        public ActionResult trenAnalysisByItems()
        {
            UserBusinessLogic BL = new UserBusinessLogic();
            ViewBag.catalogue = BL.getAllCatalogue();
            return View();
        }

        public ActionResult trenAnalysis(string dropDown1, DateTime date1, DateTime date2)
        {
            CatalogueBusinessLogic bl = new CatalogueBusinessLogic();
            List<Department> dep = bl.getDepartments();
            ReportsController depManager = new ReportsController();


            foreach (Department d in dep)
            {
                depManager.itemsDepSpendings(date1, date2, d.DepartmentID, dropDown1);
            }

            ViewBag.dataSCI = JsonConvert.SerializeObject(depManager.dataSCI);
            ViewBag.dataCOMM = JsonConvert.SerializeObject(depManager.dataCOMM);
            ViewBag.dataCPSC = JsonConvert.SerializeObject(depManager.dataCPSC);
            ViewBag.dataENGL = JsonConvert.SerializeObject(depManager.dataENGL);
            ViewBag.dataREGR = JsonConvert.SerializeObject(depManager.dataREGR);
            ViewBag.dataZOOL = JsonConvert.SerializeObject(depManager.dataZOOL);
            ViewBag.months = JsonConvert.SerializeObject(depManager.datamonths);
            return View("ChargeBackReport");

        }

        public ActionResult ViewAdjustmentVoucherItems(int adjustmentID)
        {

            new AdjustmentBusinessLogic().getAllAdjItems(adjustmentID);
            return View();

        }

        [HttpPost]
        public ActionResult SaveRequestStatusManager()
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