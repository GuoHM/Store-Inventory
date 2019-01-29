using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Script.Serialization;
using InventoryBusinessLogic;
using InventoryBusinessLogic.Entity;
using Newtonsoft.Json;
using Microsoft.AspNet.Identity;
using System.Web.Script.Serialization;


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
            string userId = User.Identity.GetUserId();
            ViewBag.depList = BL.getDepUsers(userId);
            return View();
        }

        public ActionResult saveNewRep(string dropdown1)
        {
            BL.UpdateDepRep(dropdown1);
            //EmailBusinessLogic emailBusinessLogic = new EmailBusinessLogic();
            //string content = emailBusinessLogic.ChangeDeptRepNotification(dropdown1);

            //List<string> toAddress = new List<string>();
            //toAddress.Add("wangxiaoxiaoqiang@gmail.com");
            //emailBusinessLogic.SendEmail("Team3", content, toAddress);
            return RedirectToAction("AssignDepRep");

        }

        public ActionResult AssignDepHead()
        {
            string userId = User.Identity.GetUserId();
            ViewBag.depHead = BL.appointNewDepHead(userId);
            return View();
        }

        public ActionResult saveDepHead(string dropdown1, DateTime date1, DateTime date2)
        {
            BL.UpdateDepHead(dropdown1, date1, date2);
            //EmailBusinessLogic emailBusinessLogic = new EmailBusinessLogic();
            //string content = emailBusinessLogic.ChangeDeptHeadNotification(dropdown1);

            //List<string> toAddress = new List<string>();
            //toAddress.Add("wangxiaoxiaoqiang@gmail.com");
            //emailBusinessLogic.SendEmail("Team3", content, toAddress);
            return RedirectToAction("AssignDepHead");

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
            //EmailBusinessLogic emailBusinessLogic = new EmailBusinessLogic();
            //int requestID = Convert.ToInt32(item1.orderId);
            //string content = emailBusinessLogic.ApproveOrRejectNotification(requestID);

            //List<string> toAddress = new List<string>();
            //toAddress.Add("wangxiaoxiaoqiang@gmail.com");
            //emailBusinessLogic.SendEmail("Team3", content, toAddress);
            return new JsonResult();
        }

        public ActionResult DepSpendingHistory()
        {
            return View();
        }

        public ActionResult spendingHistory(DateTime date1,DateTime date2)
        {
            string userId = User.Identity.GetUserId();
            List<Order> spendings = BL.getDepSpendingHistory(date1,date2,userId);
            decimal[] money = new decimal[12];
            List<Object> months = new List<Object>();
            List<decimal> datapoints2 = new List<decimal>();

            for (int i = 0; i < spendings.Count; i++)
            {
                DateTime myval = (DateTime)spendings[i].OrderDate;
                string month = myval.Month.ToString();
                

                if (month == "1")
                {

                    if (!(months.Contains("Jan")))
                    {
                        months.Add("Jan");
                        datapoints2.Add((decimal)spendings[i].TotalPrice);
                        
                    }
                    else
                    {
                        
                        datapoints2[datapoints2.Count - 1] += (decimal)spendings[i].TotalPrice;

                    }
                }
                else if (month == "2")
                {
                    if (!(months.Contains("Feb")))
                    {
                        months.Add("Feb");
                        datapoints2.Add((decimal)spendings[i].TotalPrice);
                    }
                    else
                    {
                        datapoints2[datapoints2.Count - 1] += (decimal)spendings[i].TotalPrice;

                    }
                }
                else if (month == "3")
                {

                    if (!(months.Contains("Mar")))
                    {
                        months.Add("Mar");
                        datapoints2.Add((decimal)spendings[i].TotalPrice);
                    }
                    else
                    {
                        
                        datapoints2[datapoints2.Count - 1] += (decimal)spendings[i].TotalPrice;
                        
                    }
                }

                else if (month == "4")
                {

                    if (!(months.Contains("Apr")))
                    {
                        months.Add("Apr");
                        datapoints2.Add((decimal)spendings[i].TotalPrice);
                    }
                    else
                    {
                        datapoints2[datapoints2.Count - 1] += (decimal)spendings[i].TotalPrice;

                    }
                }

                else if (month == "5")
                {
                    if (!(months.Contains("May")))
                    {
                        months.Add("May");
                        datapoints2.Add((decimal)spendings[i].TotalPrice);
                    }
                    else
                    {
                        datapoints2[datapoints2.Count - 1] += (decimal)spendings[i].TotalPrice;

                    }
                }

                else if (month == "6")
                {
                    if (!(months.Contains("Jun")))
                    {
                        months.Add("Jun");
                        datapoints2.Add((decimal)spendings[i].TotalPrice);
                    }
                    else
                    {
                        datapoints2[datapoints2.Count - 1] += (decimal)spendings[i].TotalPrice;

                    }
                }

                else if (month == "7")
                {
                    if (!(months.Contains("Jul")))
                    {
                        months.Add("Jul");
                        datapoints2.Add((decimal)spendings[i].TotalPrice);
                    }
                    else
                    {
                        datapoints2[datapoints2.Count - 1] += (decimal)spendings[i].TotalPrice;

                    }
                }

                else if (month == "8")
                {
                    if (!(months.Contains("Aug")))
                    {
                        months.Add("Aug");
                        datapoints2.Add((decimal)spendings[i].TotalPrice);
                    }
                    else
                    {
                        datapoints2[datapoints2.Count - 1] += (decimal)spendings[i].TotalPrice;

                    }
                }

                else if (month == "9")
                {
                    if (!(months.Contains("Sep")))
                    {
                        months.Add("Sep");
                        datapoints2.Add((decimal)spendings[i].TotalPrice);
                    }
                    else
                    {
                        datapoints2[datapoints2.Count - 1] += (decimal)spendings[i].TotalPrice;

                    }
                }
                else if (month == "10")
                {
                    if (!(months.Contains("Oct")))
                    {
                        months.Add("Oct");
                        datapoints2.Add((decimal)spendings[i].TotalPrice);
                    }
                    else
                    {
                        datapoints2[datapoints2.Count - 1] += (decimal)spendings[i].TotalPrice;

                    }
                }
                else if (month == "11")
                {
                    if (!(months.Contains("Nov")))
                    {
                        months.Add("Nov");
                        datapoints2.Add((decimal)spendings[i].TotalPrice);
                    }
                    else
                    {
                        datapoints2[datapoints2.Count - 1] += (decimal)spendings[i].TotalPrice;

                    }
                }

                else
                {
                    if (!(months.Contains("Dec")))
                    {
                        months.Add("Dec");
                        datapoints2.Add((decimal)spendings[i].TotalPrice);
                    }
                    else
                    {
                        datapoints2[datapoints2.Count - 1] += (decimal)spendings[i].TotalPrice;

                    }
                }
            }

           

            ViewBag.datapoints2 = JsonConvert.SerializeObject(datapoints2);
            ViewBag.datapoints3 = JsonConvert.SerializeObject(months);
            return View("DepSpendingHistory");
        }

        public ActionResult dashBoard()
        {
            string userId = User.Identity.GetUserId();
            List<Request> var1 = BL.getPendigRequest(userId);
            List<Request> var2 = BL.getApproveorRejected(userId);
            int count = 0;
            int rejected = 0;
            int approved = 0;

            List<String> array1 = new List<string>();
            List<String> array2 = new List<string>();
           

            foreach (Request r in var1)
            {
                if (!(array1.Contains(r.UserID)))
                {
                    array1.Add(r.UserID);
                    count++;
                }
            }
            foreach (Request r in var2)
            {
                    if (r.RequestStatus == "Rejected")
                    {
                        rejected++;
                    }
                    else { approved++; }
                
            }
            ViewBag.pendingRequest = count;
            ViewBag.rejected = rejected;
            ViewBag.approved = approved;
            return View();
        }





        public string requestStatus { get; set; }
            public string remarks { get; set; }
        }
    }

