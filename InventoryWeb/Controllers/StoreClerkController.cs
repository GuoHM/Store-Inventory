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

        public ActionResult UpdateInventory(string label1, string binNumber)
        {
            CatalogueBusinessLogic CB = new CatalogueBusinessLogic();
            CB.UpdateInventory(label1, binNumber);


            return View("ManageInventory");
        }

        public ActionResult generateChargeBack()
        {
            return View();
        }

        public ActionResult ChargeBackReport(DateTime date1, DateTime date2)
        {
            CatalogueBusinessLogic CB = new CatalogueBusinessLogic();
            List<Order> orderList = CB.depSpendings(date1, date2);

            decimal[] money = new decimal[12];
            decimal[] COMM = new decimal[12];
            decimal[] CPSC = new decimal[12];
            decimal[] ENGL = new decimal[12];
            decimal[] REGR = new decimal[12];
            decimal[] ZOOL = new decimal[12];


            for (int i = 0; i < orderList.Count; i++)
            {
                string department = orderList[i].DepartmentID;

                if (department == "1001")
                {
                    DateTime myval = (DateTime)orderList[i].OrderDate;
                    string month = myval.Month.ToString();

                    if (month == "1")
                    {

                        money[0] += (decimal)orderList[i].TotalPrice;
                    }
                    else if (month == "2")
                    {

                        money[1] += (decimal)orderList[i].TotalPrice;
                    }
                    else if (month == "3")
                    {

                        money[2] += (decimal)orderList[i].TotalPrice;
                    }

                    else if (month == "4")
                    {

                        money[3] += (decimal)orderList[i].TotalPrice;
                    }

                    else if (month == "5")
                    {

                        money[4] += (decimal)orderList[i].TotalPrice;
                    }

                    else if (month == "6")
                    {

                        money[5] += (decimal)orderList[i].TotalPrice;
                    }

                    else if (month == "7")
                    {

                        money[6] += (decimal)orderList[i].TotalPrice;
                    }

                    else if (month == "8")
                    {

                        money[7] += (decimal)orderList[i].TotalPrice;
                    }
                    else if (month == "9")
                    {

                        money[8] += (decimal)orderList[i].TotalPrice;
                    }
                    else if (month == "10")
                    {

                        money[9] += (decimal)orderList[i].TotalPrice;
                    }
                    else if (month == "11")
                    {

                        money[10] += (decimal)orderList[i].TotalPrice;
                    }

                    else
                    {

                        money[11] += (decimal)orderList[i].TotalPrice;
                    }

                }
                else if (department == "COMM\r\n")
                {
                    DateTime myval = (DateTime)orderList[i].OrderDate;
                    string month = myval.Month.ToString();

                    if (month == "1")
                    {

                        COMM[0] += (decimal)orderList[i].TotalPrice;
                    }
                    else if (month == "2")
                    {

                        COMM[1] += (decimal)orderList[i].TotalPrice;
                    }
                    else if (month == "3")
                    {

                        COMM[2] += (decimal)orderList[i].TotalPrice;
                    }

                    else if (month == "4")
                    {

                        COMM[3] += (decimal)orderList[i].TotalPrice;
                    }

                    else if (month == "5")
                    {

                        COMM[4] += (decimal)orderList[i].TotalPrice;
                    }

                    else if (month == "6")
                    {

                        COMM[5] += (decimal)orderList[i].TotalPrice;
                    }

                    else if (month == "7")
                    {

                        COMM[6] += (decimal)orderList[i].TotalPrice;
                    }

                    else if (month == "8")
                    {

                        COMM[7] += (decimal)orderList[i].TotalPrice;
                    }
                    else if (month == "9")
                    {

                        COMM[8] += (decimal)orderList[i].TotalPrice;
                    }
                    else if (month == "10")
                    {

                        COMM[9] += (decimal)orderList[i].TotalPrice;
                    }
                    else if (month == "11")
                    {

                        COMM[10] += (decimal)orderList[i].TotalPrice;
                    }

                    else
                    {

                        COMM[11] += (decimal)orderList[i].TotalPrice;
                    }
                }
                else if (department == "CPSC\r\n")
                {

                    DateTime myval = (DateTime)orderList[i].OrderDate;
                    string month = myval.Month.ToString();

                    if (month == "1")
                    {

                        CPSC[0] += (decimal)orderList[i].TotalPrice;
                    }
                    else if (month == "2")
                    {

                        CPSC[1] += (decimal)orderList[i].TotalPrice;
                    }
                    else if (month == "3")
                    {

                        CPSC[2] += (decimal)orderList[i].TotalPrice;
                    }

                    else if (month == "4")
                    {

                        CPSC[3] += (decimal)orderList[i].TotalPrice;
                    }

                    else if (month == "5")
                    {

                        CPSC[4] += (decimal)orderList[i].TotalPrice;
                    }

                    else if (month == "6")
                    {

                        CPSC[5] += (decimal)orderList[i].TotalPrice;
                    }

                    else if (month == "7")
                    {

                        CPSC[6] += (decimal)orderList[i].TotalPrice;
                    }

                    else if (month == "8")
                    {

                        CPSC[7] += (decimal)orderList[i].TotalPrice;
                    }
                    else if (month == "9")
                    {

                        CPSC[8] += (decimal)orderList[i].TotalPrice;
                    }
                    else if (month == "10")
                    {

                        CPSC[9] += (decimal)orderList[i].TotalPrice;
                    }
                    else if (month == "11")
                    {

                        CPSC[10] += (decimal)orderList[i].TotalPrice;
                    }

                    else
                    {

                        CPSC[11] += (decimal)orderList[i].TotalPrice;
                    }
                }
                else if (department == "ENGL\r\n")
                {
                    DateTime myval = (DateTime)orderList[i].OrderDate;
                    string month = myval.Month.ToString();

                    if (month == "1")
                    {

                        ENGL[0] += (decimal)orderList[i].TotalPrice;
                    }
                    else if (month == "2")
                    {

                        ENGL[1] += (decimal)orderList[i].TotalPrice;
                    }
                    else if (month == "3")
                    {

                        ENGL[2] += (decimal)orderList[i].TotalPrice;
                    }

                    else if (month == "4")
                    {

                        ENGL[3] += (decimal)orderList[i].TotalPrice;
                    }

                    else if (month == "5")
                    {

                        ENGL[4] += (decimal)orderList[i].TotalPrice;
                    }

                    else if (month == "6")
                    {

                        ENGL[5] += (decimal)orderList[i].TotalPrice;
                    }

                    else if (month == "7")
                    {

                        ENGL[6] += (decimal)orderList[i].TotalPrice;
                    }

                    else if (month == "8")
                    {

                        ENGL[7] += (decimal)orderList[i].TotalPrice;
                    }
                    else if (month == "9")
                    {

                        ENGL[8] += (decimal)orderList[i].TotalPrice;
                    }
                    else if (month == "10")
                    {

                        ENGL[9] += (decimal)orderList[i].TotalPrice;
                    }
                    else if (month == "11")
                    {

                        ENGL[10] += (decimal)orderList[i].TotalPrice;
                    }

                    else
                    {

                        ENGL[11] += (decimal)orderList[i].TotalPrice;
                    }
                }
                else if (department == "REGR\r\n")
                {
                    DateTime myval = (DateTime)orderList[i].OrderDate;
                    string month = myval.Month.ToString();

                    if (month == "1")
                    {

                        REGR[0] += (decimal)orderList[i].TotalPrice;
                    }
                    else if (month == "2")
                    {

                        REGR[1] += (decimal)orderList[i].TotalPrice;
                    }
                    else if (month == "3")
                    {

                        REGR[2] += (decimal)orderList[i].TotalPrice;
                    }

                    else if (month == "4")
                    {

                        REGR[3] += (decimal)orderList[i].TotalPrice;
                    }

                    else if (month == "5")
                    {

                        REGR[4] += (decimal)orderList[i].TotalPrice;
                    }

                    else if (month == "6")
                    {

                        REGR[5] += (decimal)orderList[i].TotalPrice;
                    }

                    else if (month == "7")
                    {

                        REGR[6] += (decimal)orderList[i].TotalPrice;
                    }

                    else if (month == "8")
                    {

                        REGR[7] += (decimal)orderList[i].TotalPrice;
                    }
                    else if (month == "9")
                    {

                        REGR[8] += (decimal)orderList[i].TotalPrice;
                    }
                    else if (month == "10")
                    {

                        REGR[9] += (decimal)orderList[i].TotalPrice;
                    }
                    else if (month == "11")
                    {

                        REGR[10] += (decimal)orderList[i].TotalPrice;
                    }

                    else
                    {

                        REGR[11] += (decimal)orderList[i].TotalPrice;
                    }
                }
                else
                {
                    DateTime myval = (DateTime)orderList[i].OrderDate;
                    string month = myval.Month.ToString();

                    if (month == "1")
                    {

                        ZOOL[0] += (decimal)orderList[i].TotalPrice;
                    }
                    else if (month == "2")
                    {

                        ZOOL[1] += (decimal)orderList[i].TotalPrice;
                    }
                    else if (month == "3")
                    {

                        ZOOL[2] += (decimal)orderList[i].TotalPrice;
                    }

                    else if (month == "4")
                    {

                        ZOOL[3] += (decimal)orderList[i].TotalPrice;
                    }

                    else if (month == "5")
                    {

                        ZOOL[4] += (decimal)orderList[i].TotalPrice;
                    }

                    else if (month == "6")
                    {

                        ZOOL[5] += (decimal)orderList[i].TotalPrice;
                    }

                    else if (month == "7")
                    {

                        ZOOL[6] += (decimal)orderList[i].TotalPrice;
                    }

                    else if (month == "8")
                    {

                        ZOOL[7] += (decimal)orderList[i].TotalPrice;
                    }
                    else if (month == "9")
                    {

                        ZOOL[8] += (decimal)orderList[i].TotalPrice;
                    }
                    else if (month == "10")
                    {

                        ZOOL[9] += (decimal)orderList[i].TotalPrice;
                    }
                    else if (month == "11")
                    {

                        ZOOL[10] += (decimal)orderList[i].TotalPrice;
                    }

                    else
                    {

                        ZOOL[11] += (decimal)orderList[i].TotalPrice;
                    }
                }
            }

            ViewBag.datapoints2 = JsonConvert.SerializeObject(money);
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
            ViewBag.Itemcode = dropDown1;
            

            int[] money = new int[12];
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

                if (department == "1001")
                {
                    DateTime myval = (DateTime)req[i].RequestDate;
                    string month = myval.Month.ToString();

                    if (month == "1")
                    {

                        money[0] += (int)req[i].Needed;
                    }
                    else if (month == "2")
                    {

                        money[1] += (int)req[i].Needed;
                    }
                    else if (month == "3")
                    {

                        money[2] += (int)req[i].Needed;
                    }

                    else if (month == "4")
                    {

                        money[3] += (int)req[i].Needed;
                    }

                    else if (month == "5")
                    {

                        money[4] += (int)req[i].Needed;
                    }

                    else if (month == "6")
                    {

                        money[5] += (int)req[i].Needed;
                    }

                    else if (month == "7")
                    {

                        money[6] += (int)req[i].Needed;
                    }

                    else if (month == "8")
                    {

                        money[7] += (int)req[i].Needed;
                    }
                    else if (month == "9")
                    {

                        money[8] += (int)req[i].Needed;
                    }
                    else if (month == "10")
                    {

                        money[9] += (int)req[i].Needed;
                    }
                    else if (month == "11")
                    {

                        money[10] += (int)req[i].Needed;
                    }
                    else
                    
                    {

                        money[11] += (int)req[i].Needed;
                    }
                  

                }

                else if (department == "COMM\r\n")
                {
                    DateTime myval = (DateTime)req[i].RequestDate;
                    string month = myval.Month.ToString();

                    if (month == "1")
                    {

                        COMM[0] += (int)req[i].Needed;
                    }
                    else if (month == "2")
                    {

                        COMM[1] += (int)req[i].Needed;
                    }
                    else if (month == "3")
                    {

                        COMM[2] += (int)req[i].Needed;
                    }

                    else if (month == "4")
                    {

                        COMM[3] += (int)req[i].Needed;
                    }

                    else if (month == "5")
                    {

                        COMM[4] += (int)req[i].Needed;
                    }

                    else if (month == "6")
                    {

                        COMM[5] += (int)req[i].Needed;
                    }

                    else if (month == "7")
                    {

                        COMM[6] += (int)req[i].Needed;
                    }

                    else if (month == "8")
                    {

                        COMM[7] += (int)req[i].Needed;
                    }
                    else if (month == "9")
                    {

                        COMM[8] += (int)req[i].Needed;
                    }
                    else if (month == "10")
                    {

                        COMM[9] += (int)req[i].Needed;
                    }
                    else if (month == "11")
                    {

                        COMM[10] += (int)req[i].Needed;
                    }

                    else
                    {

                        COMM[11] += (int)req[i].Needed;
                    }

                   
                }
                else if (department == "ENGL\r\n")
                {
                    DateTime myval = (DateTime)req[i].RequestDate;
                    string month = myval.Month.ToString();

                    if (month == "1")
                    {

                        ENGL[0] += (int)req[i].Needed;
                    }
                    else if (month == "2")
                    {

                        ENGL[1] += (int)req[i].Needed;
                    }
                    else if (month == "3")
                    {

                        ENGL[2] += (int)req[i].Needed;
                    }

                    else if (month == "4")
                    {

                        ENGL[3] += (int)req[i].Needed;
                    }

                    else if (month == "5")
                    {

                        ENGL[4] += (int)req[i].Needed;
                    }

                    else if (month == "6")
                    {

                        ENGL[5] += (int)req[i].Needed;
                    }

                    else if (month == "7")
                    {

                        ENGL[6] += (int)req[i].Needed;
                    }

                    else if (month == "8")
                    {

                        ENGL[7] += (int)req[i].Needed;
                    }
                    else if (month == "9")
                    {

                        ENGL[8] += (int)req[i].Needed;
                    }
                    else if (month == "10")
                    {

                        ENGL[9] += (int)req[i].Needed;
                    }
                    else if (month == "11")
                    {

                        ENGL[10] += (int)req[i].Needed;
                    }

                    else
                    {

                        ENGL[11] += (int)req[i].Needed;
                    }

                   



                }
                else if (department == "CPSC\r\n")
                {
                    DateTime myval = (DateTime)req[i].RequestDate;
                    string month = myval.Month.ToString();

                    if (month == "1")
                    {

                        CPSC[0] += (int)req[i].Needed;
                    }
                    else if (month == "2")
                    {

                        CPSC[1] += (int)req[i].Needed;
                    }
                    else if (month == "3")
                    {

                        CPSC[2] += (int)req[i].Needed;
                    }

                    else if (month == "4")
                    {

                        CPSC[3] += (int)req[i].Needed;
                    }

                    else if (month == "5")
                    {

                        CPSC[4] += (int)req[i].Needed;
                    }

                    else if (month == "6")
                    {

                        CPSC[5] += (int)req[i].Needed;
                    }

                    else if (month == "7")
                    {

                        CPSC[6] += (int)req[i].Needed;
                    }

                    else if (month == "8")
                    {

                        CPSC[7] += (int)req[i].Needed;
                    }
                    else if (month == "9")
                    {

                        CPSC[8] += (int)req[i].Needed;
                    }
                    else if (month == "10")
                    {

                        CPSC[9] += (int)req[i].Needed;
                    }
                    else if (month == "11")
                    {

                        CPSC[10] += (int)req[i].Needed;
                    }

                    else
                    {

                        CPSC[11] += (int)req[i].Needed;
                    }
                   
                }
                else if (department == "REGR\r\n")
                {
                    DateTime myval = (DateTime)req[i].RequestDate;
                    string month = myval.Month.ToString();

                    if (month == "1")
                    {

                        REGR[0] += (int)req[i].Needed;
                    }
                    else if (month == "2")
                    {

                        REGR[1] += (int)req[i].Needed;
                    }
                    else if (month == "3")
                    {

                        REGR[2] += (int)req[i].Needed;
                    }

                    else if (month == "4")
                    {

                        REGR[3] += (int)req[i].Needed;
                    }

                    else if (month == "5")
                    {

                        REGR[4] += (int)req[i].Needed;
                    }

                    else if (month == "6")
                    {

                        REGR[5] += (int)req[i].Needed;
                    }

                    else if (month == "7")
                    {

                        REGR[6] += (int)req[i].Needed;
                    }

                    else if (month == "8")
                    {

                        REGR[7] += (int)req[i].Needed;
                    }
                    else if (month == "9")
                    {

                        REGR[8] += (int)req[i].Needed;
                    }
                    else if (month == "10")
                    {

                        REGR[9] += (int)req[i].Needed;
                    }
                    else if (month == "11")
                    {

                        REGR[10] += (int)req[i].Needed;
                    }

                    else
                    {

                        REGR[11] += (int)req[i].Needed;
                    }
                    
                }
                else if (department == "ZOOL\r\n")
                {
                    DateTime myval = (DateTime)req[i].RequestDate;
                    string month = myval.Month.ToString();

                    if (month == "1")
                    {

                        ZOOL[0] += (int)req[i].Needed;
                    }
                    else if (month == "2")
                    {

                        ZOOL[1] += (int)req[i].Needed;
                    }
                    else if (month == "3")
                    {

                        ZOOL[2] += (int)req[i].Needed;
                    }

                    else if (month == "4")
                    {

                        ZOOL[3] += (int)req[i].Needed;
                    }

                    else if (month == "5")
                    {

                        ZOOL[4] += (int)req[i].Needed;
                    }

                    else if (month == "6")
                    {

                        ZOOL[5] += (int)req[i].Needed;
                    }

                    else if (month == "7")
                    {

                        ZOOL[6] += (int)req[i].Needed;
                    }

                    else if (month == "8")
                    {

                        ZOOL[7] += (int)req[i].Needed;
                    }
                    else if (month == "9")
                    {

                        ZOOL[8] += (int)req[i].Needed;
                    }
                    else if (month == "10")
                    {

                        ZOOL[9] += (int)req[i].Needed;
                    }
                    else if (month == "11")
                    {

                        ZOOL[10] += (int)req[i].Needed;
                    }

                    else
                    {

                        ZOOL[11] += (int)req[i].Needed;
                    }

                   





                }

              
            }

            ViewBag.datapoints2 = JsonConvert.SerializeObject(money);
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
            }
            return new JsonResult();
        }
        public ActionResult AddItems()
        {
            return View();

        }
        public void UpdateQuantity(List<orderIDList> purchaseIDList)
        {
            foreach (orderIDList oId in purchaseIDList)
            {

                int orderID = Convert.ToInt32(oId.orderid);
                catalogueBusinessLogic.UpdateCataloguesByPurchaseID(orderID);
            }
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


    }

    class PurchaseItemList
    {
        public string itemID { get; set; }
        public string description { get; set; }
        public string quantity { get; set; }
        public string price { get; set; }
        public string amount { get; set; }
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


}



