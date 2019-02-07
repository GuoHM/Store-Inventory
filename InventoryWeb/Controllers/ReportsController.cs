using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using InventoryBusinessLogic;
using InventoryBusinessLogic.Entity;
using Newtonsoft.Json;
using Microsoft.AspNet.Identity;

namespace InventoryWeb.Controllers
{
    public class ReportsController : Controller
    {

        public List<decimal> dataSCI = new List<decimal>();
        public List<decimal> dataCOMM = new List<decimal>();
        public List<decimal> dataCPSC = new List<decimal>();
        public List<decimal> dataENGL = new List<decimal>();
        public List<decimal> dataREGR = new List<decimal>();
        public List<decimal> dataZOOL = new List<decimal>();
        public List<decimal> dataSTORE = new List<decimal>();
        public List<Object> datamonths = new List<Object>();
        UserBusinessLogic BL = new UserBusinessLogic();

        // GET: Reports
        public ActionResult Index()
        {
            return View();
        }

        public int GetMonthDifference(DateTime startDate, DateTime endDate)
        {
            int monthsApart = 12 * (startDate.Year - endDate.Year) + startDate.Month - endDate.Month;
            return Math.Abs(monthsApart) + 1;
        }

 
        public void spendingHistorytwo(DateTime date1, DateTime date2, string depID)
        {
            List<Order> spendings = BL.getOverallSpendingHistory(date1, date2, depID);
            List<Object> secondmonths = new List<Object>();
            List<String> jui = new List<string>();
            List<Object> xaxis = new List<object>();
            List<Object> years = new List<object>();
            List<decimal> datapoints2 = new List<decimal>();

            int da = GetMonthDifference(date1, date2);
            DateTime temp = new DateTime();
            temp = date1;
            
            
            for (int i = 0; i < da; i++)
            {
                
                jui.Add(temp.Year.ToString()+temp.Month.ToString());
                xaxis.Add(temp.ToString("MMM")+ temp.Year.ToString().Substring(2, 2));
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
                        datapoints2.RemoveAt(jui.IndexOf(year+month));
                        datapoints2.Insert(jui.IndexOf(year+month), (decimal)spendings[i].TotalPrice);
                       
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

            if (depID.Trim() == "1001")
            {

                dataSCI = datapoints2;

            }
            else if (depID.Trim() == "COMM")
            {

                dataCOMM = datapoints2;

            }

            else if (depID.Trim() == "CPSC")
            {

                dataCPSC = datapoints2;

            }
            else if (depID.Trim() == "ENGL")
            {

                dataENGL = datapoints2;

            }
            else if (depID.Trim() == "REGR")
            {

                dataREGR = datapoints2;

            }
            else if (depID.Trim() == "STORE")
            {

                dataSTORE = datapoints2;

            }
            else
            {

                dataZOOL = datapoints2;

            }
            datamonths = xaxis;

            
        }


        public void itemsDepSpendings(DateTime date1, DateTime date2, string depID,string dropdown)
        {


            List<Request> req = BL.getRequestOrders(date1, date2, depID,dropdown);
            List<Object> secondmonths = new List<Object>();
            List<String> jui = new List<string>();
            List<Object> xaxis = new List<object>();
            List<Object> years = new List<object>();
            List<decimal> datapoints2 = new List<decimal>();

            int da = GetMonthDifference(date1, date2);
            DateTime temp = new DateTime();
            temp = date1;
            

            for (int i = 0; i < da; i++)
            {

                jui.Add(temp.Year.ToString() + temp.Month.ToString());
                xaxis.Add(temp.ToString("MMM") + temp.Year.ToString().Substring(2, 2));
                temp = temp.AddMonths(1);
                datapoints2.Add(0);
            }

            for (int i = 0; i < req.Count; i++)
            {
                DateTime myval = (DateTime)req[i].RequestDate;
                string month = myval.Month.ToString();
                string year = myval.Year.ToString();


                if (month == "1")
                {
                    if (!(secondmonths.Contains(month + year)))
                    {

                        secondmonths.Add(month + year);
                        datapoints2.RemoveAt(jui.IndexOf(year + month));
                        datapoints2.Insert(jui.IndexOf(year + month), (decimal)req[i].Needed);
                       
                    }
                    else
                    {
                        datapoints2[jui.IndexOf(year + month)] += (decimal)req[i].Needed;

                    }

                }

                else if (month == "2")
                {
                    if (!(secondmonths.Contains(month + year)))
                    {
                        secondmonths.Add(month + year);
                        datapoints2.RemoveAt(jui.IndexOf(year + month));
                        datapoints2.Insert(jui.IndexOf(year + month), (decimal)req[i].Needed);
                    }
                    else
                    {
                        datapoints2[jui.IndexOf(year + month)] += (decimal)req[i].Needed;

                    }
                }
                else if (month == "3")
                {

                    if (!(secondmonths.Contains(month + year)))
                    {

                        secondmonths.Add(month + year);
                        datapoints2.RemoveAt(jui.IndexOf(year + month));
                        datapoints2.Insert(jui.IndexOf(year + month), (decimal)req[i].Needed);
                    }
                    else
                    {

                        datapoints2[jui.IndexOf(year + month)] += (decimal)req[i].Needed;

                    }
                }

                else if (month == "4")
                {

                    if (!(secondmonths.Contains(month + year)))
                    {

                        secondmonths.Add(month + year);
                        datapoints2.RemoveAt(jui.IndexOf(year + month));
                        datapoints2.Insert(jui.IndexOf(year + month), (decimal)req[i].Needed);
                    }
                    else
                    {
                        datapoints2[jui.IndexOf(year + month)] += (decimal)req[i].Needed;

                    }
                }

                else if (month == "5")
                {
                    if (!(secondmonths.Contains(month + year)))
                    {

                        secondmonths.Add(month + year);
                        datapoints2.RemoveAt(jui.IndexOf(year + month));
                        datapoints2.Insert(jui.IndexOf(year + month), (decimal)req[i].Needed);
                    }
                    else
                    {
                        datapoints2[jui.IndexOf(year + month)] += (decimal)req[i].Needed;

                    }
                }

                else if (month == "6")
                {
                    if (!(secondmonths.Contains(month + year)))
                    {

                        secondmonths.Add(month + year);
                        datapoints2.RemoveAt(jui.IndexOf(year + month));
                        datapoints2.Insert(jui.IndexOf(year + month), (decimal)req[i].Needed);
                    }
                    else
                    {
                        datapoints2[jui.IndexOf(year + month)] += (decimal)req[i].Needed;

                    }
                }

                else if (month == "7")
                {
                    if (!(secondmonths.Contains(month + year)))
                    {

                        secondmonths.Add(month + year);
                        datapoints2.RemoveAt(jui.IndexOf(year + month));
                        datapoints2.Insert(jui.IndexOf(year + month), (decimal)req[i].Needed);
                    }
                    else
                    {
                        datapoints2[jui.IndexOf(year + month)] += (decimal)req[i].Needed;

                    }
                }

                else if (month == "8")
                {
                    if (!(secondmonths.Contains(month + year)))
                    {

                        secondmonths.Add(month + year);
                        datapoints2.RemoveAt(jui.IndexOf(year + month));
                        datapoints2.Insert(jui.IndexOf(year + month), (decimal)req[i].Needed);
                    }
                    else
                    {
                        datapoints2[jui.IndexOf(year + month)] += (decimal)req[i].Needed;

                    }
                }

                else if (month == "9")
                {
                    if (!(secondmonths.Contains(month + year)))
                    {

                        secondmonths.Add(month + year);
                        datapoints2.RemoveAt(jui.IndexOf(year + month));
                        datapoints2.Insert(jui.IndexOf(year + month), (decimal)req[i].Needed);
                    }
                    else
                    {
                        datapoints2[jui.IndexOf(year + month)] += (decimal)req[i].Needed;

                    }
                }
                else if (month == "10")
                {
                    if (!(secondmonths.Contains(month + year)))
                    {

                        secondmonths.Add(month + year);
                        datapoints2.RemoveAt(jui.IndexOf(year + month));
                        datapoints2.Insert(jui.IndexOf(year + month), (int)req[i].Needed);

                    }
                    else
                    {
                        datapoints2[jui.IndexOf(year + month)] += (int)req[i].Needed;

                    }
                }
                else if (month == "11")
                {
                    if (!(secondmonths.Contains(month + year)))
                    {

                        secondmonths.Add(month + year);
                        datapoints2.RemoveAt(jui.IndexOf(year + month));
                        datapoints2.Insert(jui.IndexOf(year + month), (decimal)req[i].Needed);
                    }
                    else
                    {
                        datapoints2[jui.IndexOf(year + month)] += (decimal)req[i].Needed;

                    }
                }

                else
                {
                    if (!(secondmonths.Contains(month + year)))
                    {

                        secondmonths.Add(month + year);
                        datapoints2.RemoveAt(jui.IndexOf(year + month));
                        datapoints2.Insert(jui.IndexOf(year + month), (decimal)req[i].Needed);
                    }
                    else
                    {
                        datapoints2[jui.IndexOf(year + month)] += (decimal)req[i].Needed;

                    }
                }
            }

            if (depID.Trim() == "1001")
            {

                dataSCI = datapoints2;

            }
            else if (depID.Trim() == "COMM")
            {

                dataCOMM = datapoints2;

            }

            else if (depID.Trim() == "CPSC")
            {

                dataCPSC = datapoints2;

            }
            else if (depID.Trim() == "ENGL")
            {

                dataENGL = datapoints2;

            }
            else if (depID.Trim() == "REGR")
            {

                dataREGR = datapoints2;

            }
            else if (depID.Trim() == "STORE")
            {

                dataSTORE = datapoints2;

            }
            else
            {

                dataZOOL = datapoints2;

            }
            datamonths = xaxis;
        }
    }    
}
