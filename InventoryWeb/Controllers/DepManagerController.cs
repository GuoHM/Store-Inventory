using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Script.Serialization;
using InventoryBusinessLogic;

namespace InventoryWeb.Controllers
{
    public class DepManagerController : Controller
    {
        UserBusinessLogic BL = new UserBusinessLogic();
        ManageRequestBusinessLogic req = new ManageRequestBusinessLogic();
        // GET: DepManager
        public ActionResult Index()
        {

            return View();
        }

        public ActionResult AssignDepRep()
        {
            ViewBag.depList = BL.getDepUsers();
            return View();
        }

        public ActionResult saveNewRep(string dropdown1)
        {
            BL.UpdateDepRep(dropdown1);
            return View("Index");
        }

        public ActionResult AssignDepHead()
        {
            ViewBag.depHead = BL.appointNewDepHead();
            return View();
        }

        public ActionResult saveDepHead(string dropdown1, DateTime date1, DateTime date2)
        {
            BL.UpdateDepHead(dropdown1, date1, date2);
            return View("Index");
        }

        public ActionResult ApproveOrReject()
        {
            ViewBag.reqList = req.GetRequestById(1024);
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
            var list = js.Deserialize<List<SelectedList>>(stream);

            if (list.Any())
            {
                foreach (var item in list)
                {
                    req.ApproveOrRejectRequest(item.orderId, item.requestStatus,item.remarks);
                }
            }
            return new JsonResult();
        }

        class SelectedList
        {
            public string orderId { get; set; }

            public string requestStatus { get; set; }
            public string remarks { get; set; }
        }
    }
}