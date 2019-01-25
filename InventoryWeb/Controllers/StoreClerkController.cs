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
    [Authorize(Roles ="StoreClerk")]
    public class StoreClerkController : Controller
    {
        CatalogueBusinessLogic catalogueBusinessLogic = new CatalogueBusinessLogic();
        SupplierBusinessLogic supplierBusinessLogic = new SupplierBusinessLogic();
        PurchaseOrderBusinessLogic purchaseOrderBusinessLogic = new PurchaseOrderBusinessLogic();
        PurchaseItemBusinessLogic purchaseItemBusinessLogic = new PurchaseItemBusinessLogic();
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
          
            return Json(data, JsonRequestBehavior.AllowGet);
        }
       
      
        public ActionResult GetDisbursementItems(List<DepartmentList> department)
        {
           
            DisbursementList disbursement = new DisbursementList();
            req = new List<Request>();
            var orderslist = retrievals.Select(p => p.orderid).Distinct().ToList() ;
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
                            needed += Convert.ToInt32(req.Needed);
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

                        itemList.Add(new RetrievalList { orderid = req.OrderID, itemDescription = req.Catalogue.Description, availableQuantity = Convert.ToString(req.Catalogue.Quantity), binNumber = req.Catalogue.BinNumber, neededQuantity = Convert.ToString(req.Needed), remarks = req.Remarks, requestId = Convert.ToString(req.RequestID) });
                       // alreadyexist = false;
                    }

                }


                retrievals = itemList;
                }
             return Json(new { data = retrievals }, JsonRequestBehavior.AllowGet);
            
        }
        [HttpGet]
        public JsonResult GetDisbursements()
        {
            
            var data = req.Select(p => new { itemDescription = p.Catalogue.Description, quantity = p.Needed, uom = p.Catalogue.MeasureUnit }).ToList();


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
            confirmClass.supplierAddress = supplierBusinessLogic.FindSupplierById(list[0].supplier).Address;
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
            if (confirm!=null)
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
                    totalPrice += Convert.ToInt32(item.totalPrice);
                    purchaseItem.PurchaseOrderID = purchaseOrder.PurchaseOrderID;
                    supplierID = catalogue.Supplier1;
                    purchaseItemBusinessLogic.addPurchaseItem(purchaseItem);
                }
                purchaseOrder.TotalPrice = totalPrice;
                purchaseOrderBusinessLogic.updatePurchaseOrder(purchaseOrder);

            }          
            return new JsonResult();
        }

        class SelectedList
        {
            public string itemID { get; set; }

            public string description { get; set; }

            public string quantity { get; set; }

            public string totalPrice { get; set; }

            public string supplier { get; set; }
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

        }
    }

    
}