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



namespace InventoryWeb.Controllers
{
    [Authorize(Roles = "DeptHead,StoreManager,InterimDepHead")]
    public class DepManagerController : Controller
    {
        UserBusinessLogic BL = new UserBusinessLogic();
        ManageRequestBusinessLogic req = new ManageRequestBusinessLogic();
        Inventory i = new Inventory();

        // GET: DepManager
        public ActionResult Index()
        {

            return View();
        }

        public ActionResult AssignDepRep()
        {
            string userId = User.Identity.GetUserId();
            AspNetUsers user1 = i.AspNetUsers.Where(x => x.Id == userId).First<AspNetUsers>();
            ViewBag.usertype = user1.UserType;
            ViewBag.depList = BL.getDepUsers(userId);
            return View();
        }

        [HttpGet]
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
            string interimhead = "";
            string userId = User.Identity.GetUserId();
            ViewBag.depHead = BL.appointNewDepHead(userId);

            for(int i = 0; i < ViewBag.depHead.Count; i++)
            {
                if (ViewBag.depHead[i].UserType == "InterimDepHead")
                {

                    interimhead = ViewBag.depHead[i].UserType;
                    break;
                }

            }
            
            BL.UpdateDepHead(dropdown1, date1, date2, interimhead);
            //EmailBusinessLogic emailBusinessLogic = new EmailBusinessLogic();
            //string content = emailBusinessLogic.ChangeDeptHeadNotification(dropdown1);

            //List<string> toAddress = new List<string>();
            //toAddress.Add("wangxiaoxiaoqiang@gmail.com");
            //emailBusinessLogic.SendEmail("Team3", content, toAddress);
            return RedirectToAction("AssignDepHead");

        }

        public ActionResult ApproveOrReject()
        {
            string userId = User.Identity.GetUserId();
            AspNetUsers user1 = i.AspNetUsers.Where(x => x.Id == userId).First<AspNetUsers>();
            ViewBag.usertype = user1.UserType;
            ViewBag.userID = userId;
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
            string userId = User.Identity.GetUserId();
            AspNetUsers user1 = i.AspNetUsers.Where(x => x.Id == userId).First<AspNetUsers>();
            ViewBag.usertype = user1.DepartmentID.Substring(0,4);
            return View();
        }





        public ActionResult spendingHistory(DateTime date1,DateTime date2)
        {

            string userId = User.Identity.GetUserId();
            ReportsController rep = new ReportsController();
            List<Order> spendings = BL.getDepSpendingHistory(date1, date2, userId);
            List<Object> secondmonths = new List<Object>();
            List<String> jui = new List<string>();
            List<Object> xaxis = new List<object>();
            List<Object> years = new List<object>();
            List<decimal> datapoints2 = new List<decimal>();

            int da = rep.GetMonthDifference(date1, date2);
            DateTime temp = new DateTime();
            temp = date1;

            for (int i = 0; i < da; i++)
            {

                jui.Add(temp.Year.ToString() + temp.Month.ToString());
                xaxis.Add(temp.ToString("MMM") + temp.Year.ToString().Substring(2, 2));
                temp = temp.AddMonths(1);
                datapoints2.Add(0);
            }

            for (int i = 0; i < spendings.Count; i++)
            {
                DateTime myval = (DateTime)spendings[i].OrderDate;
                string month = myval.Month.ToString();
                string year = myval.Year.ToString();


                if (month == "1")
                {
                    if (!(secondmonths.Contains(month + year)))
                    {

                        secondmonths.Add(month + year);
                        datapoints2.RemoveAt(jui.IndexOf(year + month));
                        datapoints2.Insert(jui.IndexOf(year + month), (decimal)spendings[i].TotalPrice);

                    }
                    else
                    {
                        datapoints2[jui.IndexOf(year + month)] += (decimal)spendings[i].TotalPrice;

                    }

                }

                else if (month == "2")
                {
                    if (!(secondmonths.Contains(month + year)))
                    {
                        secondmonths.Add(month + year);
                        datapoints2.RemoveAt(jui.IndexOf(year + month));
                        datapoints2.Insert(jui.IndexOf(year + month), (decimal)spendings[i].TotalPrice);

                    }
                    else
                    {
                        datapoints2[jui.IndexOf(year + month)] += (decimal)spendings[i].TotalPrice;

                    }
                }
                else if (month == "3")
                {

                    if (!(secondmonths.Contains(month + year)))
                    {

                        secondmonths.Add(month + year);
                        datapoints2.RemoveAt(jui.IndexOf(year + month));
                        datapoints2.Insert(jui.IndexOf(year + month), (decimal)spendings[i].TotalPrice);

                    }
                    else
                    {

                        datapoints2[jui.IndexOf(year + month)] += (decimal)spendings[i].TotalPrice;

                    }
                }

                else if (month == "4")
                {

                    if (!(secondmonths.Contains(month + year)))
                    {

                        secondmonths.Add(month + year);
                        datapoints2.RemoveAt(jui.IndexOf(year + month));
                        datapoints2.Insert(jui.IndexOf(year + month), (decimal)spendings[i].TotalPrice);
                    }
                    else
                    {
                        datapoints2[jui.IndexOf(year + month)] += (decimal)spendings[i].TotalPrice;

                    }
                }

                else if (month == "5")
                {
                    if (!(secondmonths.Contains(month + year)))
                    {

                        secondmonths.Add(month + year);
                        datapoints2.RemoveAt(jui.IndexOf(year + month));
                        datapoints2.Insert(jui.IndexOf(year + month), (decimal)spendings[i].TotalPrice);

                    }
                    else
                    {
                        datapoints2[jui.IndexOf(year + month)] += (decimal)spendings[i].TotalPrice;

                    }
                }

                else if (month == "6")
                {
                    if (!(secondmonths.Contains(month + year)))
                    {

                        secondmonths.Add(month + year);
                        datapoints2.RemoveAt(jui.IndexOf(year + month));
                        datapoints2.Insert(jui.IndexOf(year + month), (decimal)spendings[i].TotalPrice);
                    }
                    else
                    {
                        datapoints2[jui.IndexOf(year + month)] += (decimal)spendings[i].TotalPrice;

                    }
                }

                else if (month == "7")
                {
                    if (!(secondmonths.Contains(month + year)))
                    {

                        secondmonths.Add(month + year);
                        datapoints2.RemoveAt(jui.IndexOf(year + month));
                        datapoints2.Insert(jui.IndexOf(year + month), (decimal)spendings[i].TotalPrice);

                    }
                    else
                    {
                        datapoints2[jui.IndexOf(year + month)] += (decimal)spendings[i].TotalPrice;

                    }
                }

                else if (month == "8")
                {
                    if (!(secondmonths.Contains(month + year)))
                    {

                        secondmonths.Add(month + year);
                        datapoints2.RemoveAt(jui.IndexOf(year + month));
                        datapoints2.Insert(jui.IndexOf(year + month), (decimal)spendings[i].TotalPrice);

                    }
                    else
                    {
                        datapoints2[jui.IndexOf(year + month)] += (decimal)spendings[i].TotalPrice;

                    }
                }

                else if (month == "9")
                {
                    if (!(secondmonths.Contains(month + year)))
                    {

                        secondmonths.Add(month + year);
                        datapoints2.RemoveAt(jui.IndexOf(year + month));
                        datapoints2.Insert(jui.IndexOf(year + month), (decimal)spendings[i].TotalPrice);

                    }
                    else
                    {
                        datapoints2[jui.IndexOf(year + month)] += (decimal)spendings[i].TotalPrice;

                    }
                }
                else if (month == "10")
                {
                    if (!(secondmonths.Contains(month + year)))
                    {

                        secondmonths.Add(month + year);
                        datapoints2.RemoveAt(jui.IndexOf(year + month));
                        datapoints2.Insert(jui.IndexOf(year + month), (decimal)spendings[i].TotalPrice);

                    }
                    else
                    {
                        datapoints2[jui.IndexOf(year + month)] += (decimal)spendings[i].TotalPrice;

                    }
                }
                else if (month == "11")
                {
                    if (!(secondmonths.Contains(month + year)))
                    {

                        secondmonths.Add(month + year);
                        datapoints2.RemoveAt(jui.IndexOf(year + month));
                        datapoints2.Insert(jui.IndexOf(year + month), (decimal)spendings[i].TotalPrice);

                    }
                    else
                    {
                        datapoints2[jui.IndexOf(year + month)] += (decimal)spendings[i].TotalPrice;

                    }
                }

                else
                {
                    if (!(secondmonths.Contains(month + year)))
                    {

                        secondmonths.Add(month + year);
                        datapoints2.RemoveAt(jui.IndexOf(year + month));
                        datapoints2.Insert(jui.IndexOf(year + month), (decimal)spendings[i].TotalPrice);

                    }
                    else
                    {
                        datapoints2[jui.IndexOf(year + month)] += (decimal)spendings[i].TotalPrice;

                    }
                }
            }



            ViewBag.datapoints2 = JsonConvert.SerializeObject(datapoints2);
            ViewBag.datapoints3 = JsonConvert.SerializeObject(xaxis);
            return View("DepSpendingHistory");
        }


        public ActionResult GetUnApprovalRequest(string orderid, string userid)
        {
            ManageRequestBusinessLogic request = new ManageRequestBusinessLogic();
            var requests = request.GetRequestByOrderIdUserId(orderid, userid);
            double totalPrice = 0;
            var data = requests.Select(p => new
            {
                Description = p.Catalogue.Description,
                RequestID = p.RequestID,
                Needed = p.Needed,
                Price = p.Catalogue.Price,
                MeasureUnit = p.Catalogue.MeasureUnit,
                Total = p.Needed * p.Catalogue.Price,
               
            }).ToList();

                     
            return Json(data, JsonRequestBehavior.AllowGet);
        }
        public ActionResult dashBoard()
        {
            
            string userId = User.Identity.GetUserId();
            AspNetUsers user1 = i.AspNetUsers.Where(x => x.Id == userId).First<AspNetUsers>();
            ViewBag.usertype = user1.UserType;
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

