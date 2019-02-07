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
        ManageRequestBusinessLogic req = new ManageRequestBusinessLogic();
        CatalogueBusinessLogic catalogueBusinessLogic = new CatalogueBusinessLogic();

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
            ViewBag.dataSTORE = JsonConvert.SerializeObject(depManager.dataSTORE);
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

        public ActionResult ViewLowStock()
        {
            return View();
        }

        public ActionResult ViewCatalogueItems()
        {
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
            ViewBag.dataSTORE = JsonConvert.SerializeObject(depManager.dataSTORE);
            ViewBag.dataZOOL = JsonConvert.SerializeObject(depManager.dataZOOL);
            ViewBag.months = JsonConvert.SerializeObject(depManager.datamonths);
            return View("ChargeBackReport");

        }


        public ActionResult trenAnalysisByExpenditure()
        {
            return View();
        }

        public ActionResult trendExpenditureReport(DateTime date1, DateTime date2)
        {

            List<Department> dep = catalogueBusinessLogic.getDepartments();
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
            ViewBag.dataSTORE = JsonConvert.SerializeObject(depManager.dataSTORE);
            ViewBag.dataZOOL = JsonConvert.SerializeObject(depManager.dataZOOL);
            ViewBag.months = JsonConvert.SerializeObject(depManager.datamonths);
            return View("trenAnalysisByExpenditure");
        }

        public ActionResult ApproveOrReject()
        {
            string userId = User.Identity.GetUserId();
            ViewBag.userID = userId;
            return View();
        }

        //public ActionResult SaveRequestStatus(int reqID, string reqStatus)
        //{
        //    req.ApproveOrRejectRequest(reqID, reqStatus);
        //    return View();
        //}

        [HttpPost]
        public ActionResult SaveRequestStatus()
        {
            var sr = new System.IO.StreamReader(Request.InputStream);
            var stream = sr.ReadToEnd();
            JavaScriptSerializer js = new JavaScriptSerializer();
            var list = js.Deserialize<List<SelectedList1>>(stream);

            if (list.Any())
            {
                foreach (var item in list)
                {
                    req.ApproveOrRejectRequest(item.orderId, item.requestStatus, item.reason);
                }
            }
            var item1 = list[0];
            //EmailBusinessLogic emailBusinessLogic = new EmailBusinessLogic();
            //int requestID = Convert.ToInt32(item1.orderId);
            //string content = emailBusinessLogic.ApproveOrRejectNotification(requestID);

            //List<string> toAddress = new List<string>();
            //toAddress.Add("wangxiaoxiaoqiang@gmail.com");
            //emailBusinessLogic.SendEmail("Team3", content, toAddress);
            return new JsonResult();
        }

        public ActionResult ViewAdjustmentVoucherItems(int adjustmentID)
        {

            new AdjustmentBusinessLogic().getAllAdjItems(adjustmentID);
            return View();

        }

        public ActionResult GetUnApprovalRequest(string orderid, string userid)
        {
            ManageRequestBusinessLogic request = new ManageRequestBusinessLogic();
            var requests = request.GetRequestByOrderIdUserId(orderid, userid);
            double totalPrice = 0;
            var data = requests.Select(p => new
            {
                Description = p.Catalogue.Description,
                RequestID = p.RequestID,
                Needed = p.Needed,
                Price = p.Catalogue.Price,
                MeasureUnit = p.Catalogue.MeasureUnit,
                Total = p.Needed * p.Catalogue.Price,

            }).ToList();


            return Json(data, JsonRequestBehavior.AllowGet);
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

        public class SelectedList1
        {
            public string itemID { get; set; }

            public string description { get; set; }

            public string quantity { get; set; }

            public string totalPrice { get; set; }

            public string supplier { get; set; }

            public string price { get; set; }

            public string reason { get; set; }
            public string requestStatus { get; set; }
            public string orderId { get; set; }
        }

        PurchaseOrderBusinessLogic purchaseOrderBusinessLogic = new PurchaseOrderBusinessLogic();
        public ActionResult PurchaseOrder()
        {
            return View();
        }
    }
}