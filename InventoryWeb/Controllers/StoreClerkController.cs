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
            foreach( orderIDList oId in purchaseIDList)
            {

                int orderID = Convert.ToInt32(oId.orderid);
                catalogueBusinessLogic.UpdateCataloguesByPurchaseID(orderID);
            }
        }
        
        public JsonResult ShowPurchasedetails()
        {
           string orderIDString= Request["purchaseID"];
            int orderID = Convert.ToInt32(orderIDString);
            JsonResult json = new JsonResult();
            List<PurchaseItem> purchaseItemList= purchaseItemBusinessLogic.getItemsByPurchaseOrderID(orderID);
            List<PurchaseItemList> list = new List<PurchaseItemList>();
            foreach (PurchaseItem purchaseItem in purchaseItemList)
            {
                PurchaseItemList purchaseItemListm = new PurchaseItemList();
                purchaseItemListm.itemID = purchaseItem.ItemID;
                Catalogue catalogue = catalogueBusinessLogic.getCatalogueById(purchaseItem.ItemID);
                purchaseItemListm.description = catalogue.Description;
                purchaseItemListm.quantity = "" + purchaseItem.Quantity;
                purchaseItemListm.price =""+ catalogue.Price;
                purchaseItemListm.amount = "" + purchaseItem.Quantity * catalogue.Price;
                list.Add(purchaseItemListm);
            }
            json.Data = list;
            return json;

        }
    
        class PurchaseItemList
        {
            public string itemID { get; set; }
            public string description { get; set; }
            public string quantity { get; set; }
            public string price { get; set; }
            public string amount { get; set; }
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
                EmailBusinessLogic emailBusinessLogic = new EmailBusinessLogic();
                string content = emailBusinessLogic.SendPurchaseOrderNotification("1002");

                List<string> toAddress = new List<string>();
                toAddress.Add("wangxiaoxiaoqiang@gmail.com");
                emailBusinessLogic.SendEmail("Team3", content, toAddress);
            }
            json.Data = "success";
            return json;
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
        }
      public  class orderIDList
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


}