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
            //dep= ch.getDeptByID("1001");
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

    }
}