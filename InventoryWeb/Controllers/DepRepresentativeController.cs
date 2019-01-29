using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using InventoryBusinessLogic;
using InventoryBusinessLogic.Entity;
using System.Web.Script.Serialization;
using Microsoft.AspNet.Identity;

namespace InventoryWeb.Controllers
{
    public class DepRepresentativeController : Controller
    {
        [Authorize(Roles = "DeptRep")]
        //public object UserId { get; private set; }

        // GET: DepRepresentative/ChangeCollectionPoint
        public ActionResult ChangeCollectionPoint()
        {
            ChangeCollectionPointBusinessLogic ch = new ChangeCollectionPointBusinessLogic();
            Department dep = new Department();
            //ViewBag.Department = dep.DepartmentName;
            ViewBag.DeptList = ch.getDeptByID(User.Identity.Name);

            return View();
        }

        ChangeCollectionPointBusinessLogic CP = new ChangeCollectionPointBusinessLogic();
        //ManageRequestBusinessLogic req = new ManageRequestBusinessLogic();
        // GET: DepManager

        public ActionResult saveNewCollectionPoint(string CollectionPoint)
        {
            CP.ChangeCollectionPoint(CollectionPoint, User.Identity.Name);
            // EmailBusinessLogic emailBusinessLogic = new EmailBusinessLogic();
            //string content = emailBusinessLogic.ChangePointNotification(User.Identity.Name, CollectionPoint);

            //  List<string> toAddress = new List<string>();
            //  toAddress.Add("wangxiaoxiaoqiang@gmail.com");
            //  emailBusinessLogic.SendEmail("Team3", content, toAddress);
            return RedirectToAction("ChangeCollectionPoint");
        }
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult StationaryRequest()
        {
            return View();
        }

        public ActionResult ViewAllStationeryRequisitionsDeptRep()
        {
            string userId = User.Identity.GetUserId();
            ViewBag.userID = userId;
            new ManageRequestBusinessLogic().getAllStationeryRequest(userId);
            return View();
        }

        public ActionResult ViewAllStationeryRequisitionsByOrderIdDeptRep(string orderId)
        {
            new ManageRequestBusinessLogic().getStationaryOrderByID(orderId);
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

        static List<Department> disbursementList = new List<Department>();
        static List<RetrievalList> retrievals = new List<RetrievalList>();
        static List<Request> req = new List<Request>();
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

    }
}