using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.IO;
using System.Web.Script.Serialization;
using Microsoft.AspNet.Identity;
using InventoryBusinessLogic;
using InventoryBusinessLogic.Entity;
using Newtonsoft.Json;


namespace InventoryWeb.Controllers
{
    [Authorize(Roles = "StoreClerk")]
    public class StoreClerkController : Controller
    {
        CatalogueBusinessLogic catalogueBusinessLogic = new CatalogueBusinessLogic();
        SupplierBusinessLogic supplierBusinessLogic = new SupplierBusinessLogic();
        PurchaseOrderBusinessLogic purchaseOrderBusinessLogic = new PurchaseOrderBusinessLogic();
        PurchaseItemBusinessLogic purchaseItemBusinessLogic = new PurchaseItemBusinessLogic();
        EmailBusinessLogic emailBusinessLogic = new EmailBusinessLogic();
        AdjustmentBusinessLogic adjustmentBusinessLogic = new AdjustmentBusinessLogic();
        AdjustmentItemBusinessLogic adjustmentItemBusinessLogic = new AdjustmentItemBusinessLogic();
        UserBusinessLogic userBusinessLogic = new UserBusinessLogic();

        ManageRequestBusinessLogic manageRequests = new ManageRequestBusinessLogic();

        static List<RetrievalList> retrievals = new List<RetrievalList>();
        static List<orderlist> orders = new List<orderlist>();
        static List<Department> disbursementList = new List<Department>();
        static List<Request> req = new List<Request>();
        public ActionResult RaiseRequest()
        {
            return View();
        }

        public ActionResult PurchaseOrder()
        {
            return View();
        }

        public ActionResult AdjustmentVoucher()
        {
            return View();
        }

        public ActionResult ManageInventory()
        {
            return View();
        }

        public ActionResult UpdateInventoryBinNumber(string ItemID, string binNumber)
        {

            catalogueBusinessLogic.UpdateInventory(ItemID, binNumber);


            return View("ManageInventory");
        }

        public ActionResult generateChargeBack()
        {
            return View();
        }

        public ActionResult Reports()
        {
            return View();
        }

        public ActionResult ChargeBackReport(DateTime date1, DateTime date2)
        {
            List<Order> orderList = catalogueBusinessLogic.depSpendings(date1, date2);

            decimal[] SCI = new decimal[12];
            decimal[] COMM = new decimal[12];
            decimal[] CPSC = new decimal[12];
            decimal[] ENGL = new decimal[12];
            decimal[] REGR = new decimal[12];
            decimal[] ZOOL = new decimal[12];


            for (int i = 0; i < orderList.Count; i++)
            {
                string department = orderList[i].DepartmentID;
                DateTime myval = (DateTime)orderList[i].OrderDate;
                string month = myval.Month.ToString();

                if (month == "1")
                {
                    SCI[0] += (department == "1001") ? (decimal)orderList[i].TotalPrice : 0;
                    COMM[0] += (department == "COMM\r\n") ? (decimal)orderList[i].TotalPrice : 0;
                    CPSC[0] += (department == "CPSC\r\n") ? (decimal)orderList[i].TotalPrice : 0;
                    ENGL[0] += (department == "ENGL\r\n") ? (decimal)orderList[i].TotalPrice : 0;
                    REGR[0] += (department == "REGR\r\n") ? (decimal)orderList[i].TotalPrice : 0;
                    ZOOL[0] += (department == "ZOOL\r\n") ? (decimal)orderList[i].TotalPrice : 0;
                }
                else if (month == "2")
                {

                    SCI[1] += (department == "1001") ? (decimal)orderList[i].TotalPrice : 0;
                    COMM[1] += (department == "COMM\r\n") ? (decimal)orderList[i].TotalPrice : 0;
                    CPSC[1] += (department == "CPSC\r\n") ? (decimal)orderList[i].TotalPrice : 0;
                    ENGL[1] += (department == "ENGL\r\n") ? (decimal)orderList[i].TotalPrice : 0;
                    REGR[1] += (department == "REGR\r\n") ? (decimal)orderList[i].TotalPrice : 0;
                    ZOOL[1] += (department == "ZOOL\r\n") ? (decimal)orderList[i].TotalPrice : 0;
                }
                else if (month == "3")
                {

                    SCI[2] += (department == "1001") ? (decimal)orderList[i].TotalPrice : 0;
                    COMM[2] += (department == "COMM\r\n") ? (decimal)orderList[i].TotalPrice : 0;
                    CPSC[2] += (department == "CPSC\r\n") ? (decimal)orderList[i].TotalPrice : 0;
                    ENGL[2] += (department == "ENGL\r\n") ? (decimal)orderList[i].TotalPrice : 0;
                    REGR[2] += (department == "REGR\r\n") ? (decimal)orderList[i].TotalPrice : 0;
                    ZOOL[2] += (department == "ZOOL\r\n") ? (decimal)orderList[i].TotalPrice : 0;
                }

                else if (month == "4")
                {

                    SCI[3] += (department == "1001") ? (decimal)orderList[i].TotalPrice : 0;
                    COMM[3] += (department == "COMM\r\n") ? (decimal)orderList[i].TotalPrice : 0;
                    CPSC[3] += (department == "CPSC\r\n") ? (decimal)orderList[i].TotalPrice : 0;
                    ENGL[3] += (department == "ENGL\r\n") ? (decimal)orderList[i].TotalPrice : 0;
                    REGR[3] += (department == "REGR\r\n") ? (decimal)orderList[i].TotalPrice : 0;
                    ZOOL[3] += (department == "ZOOL\r\n") ? (decimal)orderList[i].TotalPrice : 0;
                }

                else if (month == "5")
                {

                    SCI[4] += (department == "1001") ? (decimal)orderList[i].TotalPrice : 0;
                    COMM[4] += (department == "COMM\r\n") ? (decimal)orderList[i].TotalPrice : 0;
                    CPSC[4] += (department == "CPSC\r\n") ? (decimal)orderList[i].TotalPrice : 0;
                    ENGL[4] += (department == "ENGL\r\n") ? (decimal)orderList[i].TotalPrice : 0;
                    REGR[4] += (department == "REGR\r\n") ? (decimal)orderList[i].TotalPrice : 0;
                    ZOOL[4] += (department == "ZOOL\r\n") ? (decimal)orderList[i].TotalPrice : 0;
                }

                else if (month == "6")
                {

                    SCI[5] += (department == "1001") ? (decimal)orderList[i].TotalPrice : 0;
                    COMM[5] += (department == "COMM\r\n") ? (decimal)orderList[i].TotalPrice : 0;
                    CPSC[5] += (department == "CPSC\r\n") ? (decimal)orderList[i].TotalPrice : 0;
                    ENGL[5] += (department == "ENGL\r\n") ? (decimal)orderList[i].TotalPrice : 0;
                    REGR[5] += (department == "REGR\r\n") ? (decimal)orderList[i].TotalPrice : 0;
                    ZOOL[5] += (department == "ZOOL\r\n") ? (decimal)orderList[i].TotalPrice : 0;
                }

                else if (month == "7")
                {

                    SCI[6] += (department == "1001") ? (decimal)orderList[i].TotalPrice : 0;
                    COMM[6] += (department == "COMM\r\n") ? (decimal)orderList[i].TotalPrice : 0;
                    CPSC[6] += (department == "CPSC\r\n") ? (decimal)orderList[i].TotalPrice : 0;
                    ENGL[6] += (department == "ENGL\r\n") ? (decimal)orderList[i].TotalPrice : 0;
                    REGR[6] += (department == "REGR\r\n") ? (decimal)orderList[i].TotalPrice : 0;
                    ZOOL[6] += (department == "ZOOL\r\n") ? (decimal)orderList[i].TotalPrice : 0;
                }

                else if (month == "8")
                {

                    SCI[7] += (department == "1001") ? (decimal)orderList[i].TotalPrice : 0;
                    COMM[7] += (department == "COMM\r\n") ? (decimal)orderList[i].TotalPrice : 0;
                    CPSC[7] += (department == "CPSC\r\n") ? (decimal)orderList[i].TotalPrice : 0;
                    ENGL[7] += (department == "ENGL\r\n") ? (decimal)orderList[i].TotalPrice : 0;
                    REGR[7] += (department == "REGR\r\n") ? (decimal)orderList[i].TotalPrice : 0;
                    ZOOL[7] += (department == "ZOOL\r\n") ? (decimal)orderList[i].TotalPrice : 0;
                }
                else if (month == "9")
                {

                    SCI[8] += (department == "1001") ? (decimal)orderList[i].TotalPrice : 0;
                    COMM[8] += (department == "COMM\r\n") ? (decimal)orderList[i].TotalPrice : 0;
                    CPSC[8] += (department == "CPSC\r\n") ? (decimal)orderList[i].TotalPrice : 0;
                    ENGL[8] += (department == "ENGL\r\n") ? (decimal)orderList[i].TotalPrice : 0;
                    REGR[8] += (department == "REGR\r\n") ? (decimal)orderList[i].TotalPrice : 0;
                    ZOOL[8] += (department == "ZOOL\r\n") ? (decimal)orderList[i].TotalPrice : 0;
                }
                else if (month == "10")
                {

                    SCI[9] += (department == "1001") ? (decimal)orderList[i].TotalPrice : 0;
                    COMM[9] += (department == "COMM\r\n") ? (decimal)orderList[i].TotalPrice : 0;
                    CPSC[9] += (department == "CPSC\r\n") ? (decimal)orderList[i].TotalPrice : 0;
                    ENGL[9] += (department == "ENGL\r\n") ? (decimal)orderList[i].TotalPrice : 0;
                    REGR[9] += (department == "REGR\r\n") ? (decimal)orderList[i].TotalPrice : 0;
                    ZOOL[9] += (department == "ZOOL\r\n") ? (decimal)orderList[i].TotalPrice : 0;
                }
                else if (month == "11")
                {

                    SCI[10] += (department == "1001") ? (decimal)orderList[i].TotalPrice : 0;
                    COMM[10] += (department == "COMM\r\n") ? (decimal)orderList[i].TotalPrice : 0;
                    CPSC[10] += (department == "CPSC\r\n") ? (decimal)orderList[i].TotalPrice : 0;
                    ENGL[10] += (department == "ENGL\r\n") ? (decimal)orderList[i].TotalPrice : 0;
                    REGR[10] += (department == "REGR\r\n") ? (decimal)orderList[i].TotalPrice : 0;
                    ZOOL[10] += (department == "ZOOL\r\n") ? (decimal)orderList[i].TotalPrice : 0;
                }

                else
                {

                    SCI[11] += (department == "1001") ? (decimal)orderList[i].TotalPrice : 0;
                    COMM[11] += (department == "COMM\r\n") ? (decimal)orderList[i].TotalPrice : 0;
                    CPSC[11] += (department == "CPSC\r\n") ? (decimal)orderList[i].TotalPrice : 0;
                    ENGL[11] += (department == "ENGL\r\n") ? (decimal)orderList[i].TotalPrice : 0;
                    REGR[11] += (department == "REGR\r\n") ? (decimal)orderList[i].TotalPrice : 0;
                    ZOOL[11] += (department == "ZOOL\r\n") ? (decimal)orderList[i].TotalPrice : 0;
                }

            }

            ViewBag.datapoints2 = JsonConvert.SerializeObject(SCI);
            ViewBag.datapoints3 = JsonConvert.SerializeObject(COMM);
            ViewBag.datapoints4 = JsonConvert.SerializeObject(CPSC);
            ViewBag.datapoints5 = JsonConvert.SerializeObject(ENGL);
            ViewBag.datapoints6 = JsonConvert.SerializeObject(REGR);
            ViewBag.datapoints7 = JsonConvert.SerializeObject(ZOOL);
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
            Inventory inventory = new Inventory();
            UserBusinessLogic BL = new UserBusinessLogic();
            List<Request> req = BL.getRequestOrders(dropDown1, date1, date2);


            int[] SCI = new int[12];
            int[] COMM = new int[12];
            int[] CPSC = new int[12];
            int[] ENGL = new int[12];
            int[] REGR = new int[12];
            int[] ZOOL = new int[12];


            for (int i = 0; i < req.Count; i++)
            {
                string userID = req[i].UserID;
                List<AspNetUsers> dep = inventory.AspNetUsers.Where(x => x.Id == userID).ToList<AspNetUsers>();
                string department = dep[0].DepartmentID;
                DateTime myval = (DateTime)req[i].RequestDate;
                string month = myval.Month.ToString();


                if (month == "1")
                {

                    SCI[0] += (department == "1001") ? (int)req[i].Needed : 0;
                    COMM[0] += (department == "COMM\r\n") ? (int)req[i].Needed : 0;
                    CPSC[0] += (department == "CPSC\r\n") ? (int)req[i].Needed : 0;
                    ENGL[0] += (department == "ENGL\r\n") ? (int)req[i].Needed : 0;
                    REGR[0] += (department == "REGR\r\n") ? (int)req[i].Needed : 0;
                    ZOOL[0] += (department == "ZOOL\r\n") ? (int)req[i].Needed : 0;
                }
                else if (month == "2")
                {

                    SCI[1] += (department == "1001") ? (int)req[i].Needed : 0;
                    COMM[1] += (department == "COMM\r\n") ? (int)req[i].Needed : 0;
                    CPSC[1] += (department == "CPSC\r\n") ? (int)req[i].Needed : 0;
                    ENGL[1] += (department == "ENGL\r\n") ? (int)req[i].Needed : 0;
                    REGR[1] += (department == "REGR\r\n") ? (int)req[i].Needed : 0;
                    ZOOL[1] += (department == "ZOOL\r\n") ? (int)req[i].Needed : 0;
                }
                else if (month == "3")
                {

                    SCI[2] += (department == "1001") ? (int)req[i].Needed : 0;
                    COMM[2] += (department == "COMM\r\n") ? (int)req[i].Needed : 0;
                    CPSC[2] += (department == "CPSC\r\n") ? (int)req[i].Needed : 0;
                    ENGL[2] += (department == "ENGL\r\n") ? (int)req[i].Needed : 0;
                    REGR[2] += (department == "REGR\r\n") ? (int)req[i].Needed : 0;
                    ZOOL[2] += (department == "ZOOL\r\n") ? (int)req[i].Needed : 0;
                }

                else if (month == "4")
                {

                    SCI[3] += (department == "1001") ? (int)req[i].Needed : 0;
                    COMM[3] += (department == "COMM\r\n") ? (int)req[i].Needed : 0;
                    CPSC[3] += (department == "CPSC\r\n") ? (int)req[i].Needed : 0;
                    ENGL[3] += (department == "ENGL\r\n") ? (int)req[i].Needed : 0;
                    REGR[3] += (department == "REGR\r\n") ? (int)req[i].Needed : 0;
                    ZOOL[3] += (department == "ZOOL\r\n") ? (int)req[i].Needed : 0;
                }

                else if (month == "5")
                {

                    SCI[4] += (department == "1001") ? (int)req[i].Needed : 0;
                    COMM[4] += (department == "COMM\r\n") ? (int)req[i].Needed : 0;
                    CPSC[4] += (department == "CPSC\r\n") ? (int)req[i].Needed : 0;
                    ENGL[4] += (department == "ENGL\r\n") ? (int)req[i].Needed : 0;
                    REGR[4] += (department == "REGR\r\n") ? (int)req[i].Needed : 0;
                    ZOOL[4] += (department == "ZOOL\r\n") ? (int)req[i].Needed : 0;
                }

                else if (month == "6")
                {

                    SCI[5] += (department == "1001") ? (int)req[i].Needed : 0;
                    COMM[5] += (department == "COMM\r\n") ? (int)req[i].Needed : 0;
                    CPSC[5] += (department == "CPSC\r\n") ? (int)req[i].Needed : 0;
                    ENGL[5] += (department == "ENGL\r\n") ? (int)req[i].Needed : 0;
                    REGR[5] += (department == "REGR\r\n") ? (int)req[i].Needed : 0;
                    ZOOL[5] += (department == "ZOOL\r\n") ? (int)req[i].Needed : 0;
                }

                else if (month == "7")
                {

                    SCI[6] += (department == "1001") ? (int)req[i].Needed : 0;
                    COMM[6] += (department == "COMM\r\n") ? (int)req[i].Needed : 0;
                    CPSC[6] += (department == "CPSC\r\n") ? (int)req[i].Needed : 0;
                    ENGL[6] += (department == "ENGL\r\n") ? (int)req[i].Needed : 0;
                    REGR[6] += (department == "REGR\r\n") ? (int)req[i].Needed : 0;
                    ZOOL[6] += (department == "ZOOL\r\n") ? (int)req[i].Needed : 0;
                }

                else if (month == "8")
                {

                    SCI[7] += (department == "1001") ? (int)req[i].Needed : 0;
                    COMM[7] += (department == "COMM\r\n") ? (int)req[i].Needed : 0;
                    CPSC[7] += (department == "CPSC\r\n") ? (int)req[i].Needed : 0;
                    ENGL[7] += (department == "ENGL\r\n") ? (int)req[i].Needed : 0;
                    REGR[7] += (department == "REGR\r\n") ? (int)req[i].Needed : 0;
                    ZOOL[7] += (department == "ZOOL\r\n") ? (int)req[i].Needed : 0;
                }
                else if (month == "9")
                {

                    SCI[8] += (department == "1001") ? (int)req[i].Needed : 0;
                    COMM[8] += (department == "COMM\r\n") ? (int)req[i].Needed : 0;
                    CPSC[8] += (department == "CPSC\r\n") ? (int)req[i].Needed : 0;
                    ENGL[8] += (department == "ENGL\r\n") ? (int)req[i].Needed : 0;
                    REGR[8] += (department == "REGR\r\n") ? (int)req[i].Needed : 0;
                    ZOOL[8] += (department == "ZOOL\r\n") ? (int)req[i].Needed : 0;
                }
                else if (month == "10")
                {

                    SCI[9] += (department == "1001") ? (int)req[i].Needed : 0;
                    COMM[9] += (department == "COMM\r\n") ? (int)req[i].Needed : 0;
                    CPSC[9] += (department == "CPSC\r\n") ? (int)req[i].Needed : 0;
                    ENGL[9] += (department == "ENGL\r\n") ? (int)req[i].Needed : 0;
                    REGR[9] += (department == "REGR\r\n") ? (int)req[i].Needed : 0;
                    ZOOL[9] += (department == "ZOOL\r\n") ? (int)req[i].Needed : 0;
                }
                else if (month == "11")
                {

                    SCI[10] += (department == "1001") ? (int)req[i].Needed : 0;
                    COMM[10] += (department == "COMM\r\n") ? (int)req[i].Needed : 0;
                    CPSC[10] += (department == "CPSC\r\n") ? (int)req[i].Needed : 0;
                    ENGL[10] += (department == "ENGL\r\n") ? (int)req[i].Needed : 0;
                    REGR[10] += (department == "REGR\r\n") ? (int)req[i].Needed : 0;
                    ZOOL[10] += (department == "ZOOL\r\n") ? (int)req[i].Needed : 0;
                }

                else
                {

                    SCI[11] += (department == "1001") ? (int)req[i].Needed : 0;
                    COMM[11] += (department == "COMM\r\n") ? (int)req[i].Needed : 0;
                    CPSC[11] += (department == "CPSC\r\n") ? (int)req[i].Needed : 0;
                    ENGL[11] += (department == "ENGL\r\n") ? (int)req[i].Needed : 0;
                    REGR[11] += (department == "REGR\r\n") ? (int)req[i].Needed : 0;
                    ZOOL[11] += (department == "ZOOL\r\n") ? (int)req[i].Needed : 0;
                }


            }

            ViewBag.datapoints2 = JsonConvert.SerializeObject(SCI);
            ViewBag.datapoints3 = JsonConvert.SerializeObject(COMM);
            ViewBag.datapoints4 = JsonConvert.SerializeObject(CPSC);
            ViewBag.datapoints5 = JsonConvert.SerializeObject(ENGL);
            ViewBag.datapoints6 = JsonConvert.SerializeObject(REGR);
            ViewBag.datapoints7 = JsonConvert.SerializeObject(ZOOL);
            return View("ChargeBackReport");


        }
        public ActionResult ListDept()
        {
            return View();
        }
        public ActionResult ViewOrders()
        {
            return View();
        }


        public ActionResult GetRetrievalData(List<RetrievalList> jsonlist3, List<orderlist> orderlist2)
        {
            //orders = orderlist2;
            retrievals = jsonlist3;
            return Json(new { redirecturl = "RetrievalForm" }, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public JsonResult GetDisbursementList()
        {
            disbursementList = new List<Department>();
            DisbursementList disbursement = new DisbursementList();
            foreach (RetrievalList req1 in retrievals)
            {
                List<Department> dep = disbursement.GetDisbursements(req1.orderid);
                disbursementList.AddRange(dep);

            }

            var data = disbursementList.Distinct().Select(p => new
            {
                departmentName = p.DepartmentName,
                representative = p.AspNetUsers.UserName,
                collectionPoint = p.CollectionPoint
            }).ToList();

           
            //EmailBusinessLogic emailBusinessLogic = new EmailBusinessLogic();
            
            //foreach (Department dept in disbursementList) {
                //string content = emailBusinessLogic.ReadyForCollectionPoint(dept.DepartmentID);
 
             //List<string> toAddress = new List<string>();
               // toAddress.Add("wangxiaoxiaoqiang@gmail.com");
                //emailBusinessLogic.SendEmail("Team3", content, toAddress); }
            return Json(data, JsonRequestBehavior.AllowGet);
        }


        public ActionResult GetDisbursementItems(List<DepartmentList> department)
        {

            DisbursementList disbursement = new DisbursementList();
            req = new List<Request>();
            var orderslist = retrievals.Select(p => p.orderid).Distinct().ToList();
            foreach (var req1 in orderslist)
            {
                req.AddRange(disbursement.GetDisbursementList(department[0].deptName, req1));
            }

            var data = req.Select(p => new { itemDescription = p.Catalogue.Description, quantity = p.Needed, uom = p.Catalogue.MeasureUnit }).ToList();


            return Json(data, JsonRequestBehavior.AllowGet);
            // var data  = req.Select(p => new { itemDescription = p.Catalogue.Description, quantity = p.Needed, uom=p.Catalogue.MeasureUnit });


            //return Json(data, JsonRequestBehavior.AllowGet);
            // return Json(new { redirecturl = "DisbursementList" }, JsonRequestBehavior.AllowGet);
        }



        [HttpGet]
        public JsonResult GetRetrievals()
        {
            if (retrievals.Count == 0)
            {
                List<Request> allRequests = manageRequests.GetRetrievalItems();
                //List<Request> allRequests = manageRequests.GetAllRequests();
                List<RetrievalList> itemList = new List<RetrievalList>();
                bool alreadyexist = false;
                foreach (Request req in allRequests)
                {
                    foreach (RetrievalList items in itemList)
                    {
                        alreadyexist = false;
                        if (items.itemDescription.Trim() == req.Catalogue.Description.Trim())
                        {
                            int needed = Convert.ToInt32(items.neededQuantity);
                            needed += (Convert.ToInt32(req.Needed) - Convert.ToInt32(req.Actual));

                            items.neededQuantity = Convert.ToString(needed);
                            alreadyexist = true;

                            if (Convert.ToInt32(items.neededQuantity) > req.Catalogue.Quantity)
                            {
                                items.remarks = "Not enough Stock";
                            }
                            break;
                        }



                    }
                    if (!alreadyexist)
                    {


                        itemList.Add(new RetrievalList { orderid = req.OrderID, itemDescription = req.Catalogue.Description, availableQuantity = Convert.ToString(req.Catalogue.Quantity), alreadyExisting = Convert.ToString(req.Actual), binNumber = req.Catalogue.BinNumber, neededQuantity = Convert.ToString(req.Needed), remarks = req.Remarks, requestId = Convert.ToString(req.RequestID) });
                        // alreadyexist = false;
                        // alreadyexist = false;
                    }


                }

                foreach (var item in itemList)
                {
                    item.neededQuantity = Convert.ToString(Convert.ToInt32(item.neededQuantity) - Convert.ToInt32(item.alreadyExisting));

                }
                var notneeded = itemList.Where(x => x.neededQuantity == "0").ToList();
                foreach (var item in notneeded)
                {
                    itemList.Remove(item);
                }
                retrievals = itemList;
                retrievals = itemList;
            }
            return Json(new { data = retrievals }, JsonRequestBehavior.AllowGet);

        }
        [HttpGet]
        public JsonResult GetDisbursements()
        {

            var data = req.Select(p => new { itemDescription = p.Catalogue.Description, quantity = p.Needed, uom = p.Catalogue.MeasureUnit,orderid = p.OrderID}).ToList();


            return Json(data, JsonRequestBehavior.AllowGet);
            // return Json(new { data = retrievals }, JsonRequestBehavior.AllowGet);
        }


        public ActionResult RetrievalForm()
        {

            return View();
        }

        public ActionResult DisbursementList()
        {

            return View();
        }


        public JsonResult ConfirmOrder()
        {
            var sr = new StreamReader(Request.InputStream);
            var stream = sr.ReadToEnd();
            JavaScriptSerializer js = new JavaScriptSerializer();
            var list = js.Deserialize<List<SelectedList>>(stream);
            JsonResult json = new JsonResult();
            confirmClass confirmClass = new confirmClass();
            confirmClass.tablelist = list;
            Supplier supplier = supplierBusinessLogic.FindSupplierById(list[0].supplier);
            confirmClass.supplierAddress = supplier.Address;
            confirmClass.attentionTo = supplier.SupplierName;
            json.Data = confirmClass;
            return json;
        }

        public ActionResult SavePurchaseOrder()
        {
            var sr = new StreamReader(Request.InputStream);
            var stream = sr.ReadToEnd();
            var confirm = JsonConvert.DeserializeObject<confirmClass>(stream);
            double totalPrice = 0;
            string supplierID = "";
            if (confirm != null)
            {
                var list = confirm.tablelist;
                PurchaseOrder purchaseOrder = new PurchaseOrder();
                purchaseOrder.SupplierID = confirm.tablelist.First().supplier;
                purchaseOrder.TotalPrice = 0;
                purchaseOrder.PurchaseDate = DateTime.Now;
                purchaseOrder.OrderBy = User.Identity.GetUserId();
                purchaseOrder.PurchaseOrderStatus = "Unfullfill";
                purchaseOrder.ExpectedDate = Convert.ToDateTime(confirm.dateToDeliver);
                purchaseOrder.DeliverAddress = confirm.delieverTo;
                purchaseOrder.PurchaseOrderID = purchaseOrderBusinessLogic.generatePurchaseOrderID();
                purchaseOrderBusinessLogic.addPurchaseOrder(purchaseOrder);
                //var list= confirms.First().tablelist;
                foreach (var item in list)
                {
                    Catalogue catalogue = catalogueBusinessLogic.getCatalogueById(item.itemID);
                    PurchaseItem purchaseItem = new PurchaseItem();
                    purchaseItem.ItemID = catalogue.ItemID;
                    purchaseItem.Quantity = Convert.ToInt32(item.quantity);
                    double price = Convert.ToDouble(item.totalPrice.Substring(1, item.totalPrice.Length - 1));
                    totalPrice += price;
                    purchaseItem.PurchaseOrderID = purchaseOrder.PurchaseOrderID;
                    supplierID = catalogue.Supplier1;
                    purchaseItemBusinessLogic.addPurchaseItem(purchaseItem);
                }
                purchaseOrder.TotalPrice = totalPrice;
                purchaseOrderBusinessLogic.updatePurchaseOrder(purchaseOrder);
                //EmailBusinessLogic emailBusinessLogic = new EmailBusinessLogic();
                //string content = emailBusinessLogic.SendPurchaseOrderNotification(purchaseOrder.PurchaseOrderID);

                //List<string> toAddress = new List<string>();
                //toAddress.Add("wangxiaoxiaoqiang@gmail.com");
                //emailBusinessLogic.SendEmail("Team3", content, toAddress);
            }

            return new JsonResult();
        }
        public ActionResult AddItems()
        {
            return View();

        }
        public bool UpdateQuantity(List<orderIDList> purchaseIDList)
        {
            foreach (orderIDList oId in purchaseIDList)
            {

                int orderID = Convert.ToInt32(oId.orderid);
                 catalogueBusinessLogic.UpdateCataloguesByPurchaseID(orderID);
                
            }
            return true;
        }
        public JsonResult LowStock()
        {
            JsonResult json = new JsonResult();
            json.Data = catalogueBusinessLogic.GetLowStock();
            json.JsonRequestBehavior = JsonRequestBehavior.AllowGet;
            return json;
        }

        public ActionResult SaveAdjustmentVoucher()
        {
            var sr = new StreamReader(Request.InputStream);
            var stream = sr.ReadToEnd();
            JavaScriptSerializer js = new JavaScriptSerializer();
            var list = JsonConvert.DeserializeObject<List<SelectedList>>(stream);
            JsonResult json = new JsonResult();
            if (list.Any())
            {
                Adjustment adjustment = new Adjustment();
                adjustment.UserID = User.Identity.GetUserId();
                adjustment.TotalPrice = 0;
                adjustment.Date = DateTime.Now;
                adjustment.AdjustmentID = adjustmentBusinessLogic.generateAdjustmentID();
                adjustment.AdjustmentStatus = "Unapprove";
                adjustmentBusinessLogic.addAdjustment(adjustment);
                foreach (var item in list)
                {
                    Catalogue catalogue = catalogueBusinessLogic.getCatalogueById(item.itemID);
                    double quantity = Convert.ToDouble(item.quantity);
                    if (quantity < 0 && quantity < -catalogue.Quantity)
                    {
                        json.Data = "fail";
                        return json;
                    }
                    AdjustmentItem adjustmentItem = new AdjustmentItem();
                    adjustmentItem.ItemID = catalogue.ItemID;
                    adjustmentItem.Quantity = item.quantity;
                    adjustmentItem.Reason = item.reason;
                    adjustmentItem.AdjustmentID = adjustment.AdjustmentID;
                    adjustment.TotalPrice += Math.Abs(Convert.ToInt32(catalogue.Price * Convert.ToDouble(item.quantity)));
                    adjustmentItemBusinessLogic.addAdjustmentItem(adjustmentItem);
                }
                if (adjustment.TotalPrice >= 250)
                {
                    adjustment.Supervisor = userBusinessLogic.getStoreManager().Id;
                }
                else
                {
                    adjustment.Supervisor = userBusinessLogic.getStoreStoreSupervisor().Id;
                }
                adjustmentBusinessLogic.updateAdjustment(adjustment);

                //EmailBusinessLogic emailBusinessLogic = new EmailBusinessLogic();
                //string content = emailBusinessLogic.NewVoucherNotification(adjustment.AdjustmentID,adjustment.UserID);

                // List<string> toAddress = new List<string>();
                //toAddress.Add("wangxiaoxiaoqiang@gmail.com");
                //emailBusinessLogic.SendEmail("Team3", content, toAddress);
               
            }

            json.Data = "success";
            return json;
        }

        public JsonResult ShowPurchasedetails()
        {
            string orderIDString = Request["purchaseID"];
            int orderID = Convert.ToInt32(orderIDString);
            JsonResult json = new JsonResult();
            List<PurchaseItem> purchaseItemList = purchaseItemBusinessLogic.getItemsByPurchaseOrderID(orderID);
            List<PurchaseItemList> list = new List<PurchaseItemList>();
            foreach (PurchaseItem purchaseItem in purchaseItemList)
            {
                PurchaseItemList purchaseItemListm = new PurchaseItemList();
                purchaseItemListm.itemID = purchaseItem.ItemID;
                Catalogue catalogue = catalogueBusinessLogic.getCatalogueById(purchaseItem.ItemID);
                purchaseItemListm.description = catalogue.Description;
                purchaseItemListm.quantity = "" + purchaseItem.Quantity;
                purchaseItemListm.price = "" + catalogue.Price;
                purchaseItemListm.amount = "" + purchaseItem.Quantity * catalogue.Price;
                list.Add(purchaseItemListm);
            }
            json.Data = list;
            return json;

        }

        public ActionResult ViewAllAdjustmentVoucherRaised()
        {
            string userId = User.Identity.GetUserId();
            ViewBag.userID = userId;
            new AdjustmentBusinessLogic().getAllAdjustmentList(userId);
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

       
        public JsonResult UpdatePOStatusToCancel(List<CancelPOList> Po)
        {

            
            List<CancelPOList> list = Po;
            if (list.Any())
            {
                foreach (var item in list)
                {
                    
                    new PurchaseItemBusinessLogic().getPOByID(Convert.ToInt32(item.orderid));
                }
            }
            

            return new JsonResult();
        }


        [HttpPost]
        public ActionResult UpdateInventory()
        {
            var sr = new System.IO.StreamReader(Request.InputStream);
            var stream = sr.ReadToEnd();
            JavaScriptSerializer js = new JavaScriptSerializer();
            var list = js.Deserialize<List<InventoryList>>(stream);
            if (list.Any())
            {
                foreach (var item in list)
                {
                    if (item != null)
                    {
                        catalogueBusinessLogic.UpdateRetrievedQuantity(item.itemDescription, item.quantityPicked);
                    }
                }
            }
            EmailBusinessLogic emailBusinessLogic = new EmailBusinessLogic();
            string content = emailBusinessLogic.LowStockNotification();

            List<string> toAddress = new List<string>();
            toAddress.Add("wangxiaoxiaoqiang@gmail.com");
            emailBusinessLogic.SendEmail("Team3", content, toAddress);
            catalogueBusinessLogic.ValidateOrderStatus();

            return new JsonResult();

        }


        }

        class PurchaseItemList
        {
            public string itemID { get; set; }
            public string description { get; set; }
            public string quantity { get; set; }
            public string price { get; set; }
            public string amount { get; set; }
        }

        public class InventoryList
        {
            public string itemDescription { get; set; }
            public string quantityPicked { get; set; }
        }


        public class SelectedList
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
        public class orderIDList
        {
            public string orderid { get; set; }
        }

        class confirmClass
        {
            public List<SelectedList> tablelist { get; set; }

            public string supplierAddress { get; set; }

            public string delieverTo { get; set; }

            public string attentionTo { get; set; }

            public string dateToDeliver { get; set; }

        }
        public class orderlist
        {
            public string orderid { get; set; }
        }

        public class RetrievalList
        {
            public string requestId { get; set; }
            public string itemDescription { get; set; }

            public string neededQuantity { get; set; }
            public string availableQuantity { get; set; }
        public string alreadyExisting { get; set; }

        public string binNumber { get; set; }
            public string remarks { get; set; }
            public string orderid { get; set; }
            //public string itemDescription { get; set; }

        }
        public class DepartmentList
        {
            public string deptName { get; set; }
        }

    public class DisbursementListItems
    {
        public string itemDescription { get; set; }
        public string quantity { get; set; }
        public string uom { get; set; }
        public string orderid { get; set; }

        }

        public class CancelPOList
        {
        public string orderid { get; set; }
        public string supplierID { get; set; }
        public string totalPrice { get; set; }
        public string purchaseDate { get; set; }
        public string deliverAddress { get; set; }
        public string orderBy { get; set; }
        public string expectedDate { get; set; }
        public string purchaseOrderStatus { get; set; }

        }


    }







