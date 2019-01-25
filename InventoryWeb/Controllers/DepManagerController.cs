using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Script.Serialization;
using InventoryBusinessLogic;
using InventoryBusinessLogic.Entity;
using Newtonsoft.Json;

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
            EmailBusinessLogic emailBusinessLogic = new EmailBusinessLogic();
            string content = emailBusinessLogic.ChangeDeptRepNotification(dropdown1);

            List<string> toAddress = new List<string>();
            toAddress.Add("wangxiaoxiaoqiang@gmail.com");
            emailBusinessLogic.SendEmail("Team3", content, toAddress);
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
            EmailBusinessLogic emailBusinessLogic = new EmailBusinessLogic();
            string content = emailBusinessLogic.ChangeDeptHeadNotification(dropdown1);

            List<string> toAddress = new List<string>();
            toAddress.Add("wangxiaoxiaoqiang@gmail.com");
            emailBusinessLogic.SendEmail("Team3", content, toAddress);
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
                    req.ApproveOrRejectRequest(item.orderId, item.requestStatus,item.reason);
                }
            }
            var item1 = list[0];
            EmailBusinessLogic emailBusinessLogic = new EmailBusinessLogic();
            int requestID = Convert.ToInt32(item1.orderId);
            string content = emailBusinessLogic.ApproveOrRejectNotification(requestID);

            List<string> toAddress = new List<string>();
            toAddress.Add("wangxiaoxiaoqiang@gmail.com");
            emailBusinessLogic.SendEmail("Team3", content, toAddress);
            return new JsonResult();
        }

        public ActionResult DepSpendingHistory()
        {
            return View();
        }

        public ActionResult spendingHistory(DateTime date1,DateTime date2)
        {
            List<Order> spendings = BL.getDepSpendingHistory(date1,date2);
            decimal[] money = new decimal[12];
           
            List<decimal> datapoints2 = new List<decimal>();

            for (int i = 0; i < spendings.Count; i++)
            {
                DateTime myval = (DateTime)spendings[i].OrderDate;
                string month = myval.Month.ToString();

                if (month == "1")
                {

                    money[0] += (decimal)spendings[i].TotalPrice;
                }
                else if (month == "2")
                {

                    money[1] += (decimal)spendings[i].TotalPrice;
                }
                else if (month == "3")
                {

                    money[2] += (decimal)spendings[i].TotalPrice;
                }

                else if (month == "4")
                {

                    money[3] += (decimal)spendings[i].TotalPrice;
                }

                else if (month == "5")
                {

                    money[4] += (decimal)spendings[i].TotalPrice;
                }

                else if (month == "6")
                {

                    money[5] += (decimal)spendings[i].TotalPrice;
                }

                else if (month == "7")
                {

                    money[6] += (decimal)spendings[i].TotalPrice;
                }

                else if (month == "8")
                {

                    money[7] += (decimal)spendings[i].TotalPrice;
                }
                else if (month == "9")
                {

                    money[8] += (decimal)spendings[i].TotalPrice;
                }
                else if (month == "10")
                {

                    money[9] += (decimal)spendings[i].TotalPrice;
                }
                else if (month == "11")
                {

                    money[10] += (decimal)spendings[i].TotalPrice;
                }

                else
                {
                     
                    money[11] += (decimal)spendings[i].TotalPrice;
                }
            }

            for (int i = 0; i < money.Length; i++)
            {
                if (money[i] > 0)
                {
                    datapoints2.Add(money[i]);
                }
            }
            ViewBag.datapoints2 = JsonConvert.SerializeObject(datapoints2);
            return View("DepSpendingHistory");
        }



            public string requestStatus { get; set; }
            public string remarks { get; set; }
        }
    }

