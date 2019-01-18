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

namespace InventoryWeb.Controllers
{
    [Authorize(Roles ="StoreClerk")]
    public class StoreClerkController : Controller
    {
        CatalogueBusinessLogic catalogueBusinessLogic = new CatalogueBusinessLogic();
        SupplierBusinessLogic supplierBusinessLogic = new SupplierBusinessLogic();
        PurchaseOrderBusinessLogic purchaseOrderBusinessLogic = new PurchaseOrderBusinessLogic();

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

        public ActionResult SavePurchaseOrder()
        {
            var sr = new StreamReader(Request.InputStream);
            var stream = sr.ReadToEnd();
            string username = User.Identity.GetUserId();
            JavaScriptSerializer js = new JavaScriptSerializer();
            var list = js.Deserialize<List<SelectedList>>(stream);
            double totalPrice = 0;
            string supplierID = "";
            if (list.Any())
            {
                PurchaseOrder purchaseOrder = new PurchaseOrder();
                Supplier supplier = supplierBusinessLogic.FindSupplierById(supplierID);
                purchaseOrder.SupplierID = supplierID;
                purchaseOrder.TotalPrice = 0;
                purchaseOrder.PurchaseDate = DateTime.Now;
                purchaseOrder.OrderBy = User.Identity.GetUserId();
                purchaseOrder.PurchaseOrderStatus = "Unfullfill";
                purchaseOrderBusinessLogic.addPurchaseOrder(purchaseOrder);
                foreach (var item in list)
                {
                    Catalogue catalogue = catalogueBusinessLogic.getCatalogueById(item.itemID);
                    PurchaseItem purchaseItem = new PurchaseItem();
                    purchaseItem.ItemID = catalogue.ItemID;
                    purchaseItem.Quantity = Convert.ToInt32(item.quantity);
                    totalPrice += Convert.ToInt32(item.totalPrice);
                    purchaseItem.PurchaseOrderID = purchaseOrderBusinessLogic.generatePurchaseOrderID();
                    supplierID = catalogue.Supplier1;
                }
                purchaseOrder.TotalPrice = totalPrice;

            }          
            return new JsonResult();
        }

        class SelectedList
        {
            public string itemID { get; set; }

            public string quantity { get; set; }

            public string totalPrice { get; set; }
        }
    }

    
}