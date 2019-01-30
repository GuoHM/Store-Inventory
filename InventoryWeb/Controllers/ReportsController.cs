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
        public List<Object> datamonths = new List<object>();
        UserBusinessLogic BL = new UserBusinessLogic();

        // GET: Reports
        public ActionResult Index()
        {
            return View();
        }


        public void spendingHistorytwo(DateTime date1, DateTime date2, string depID)
        {


            List<Order> spendings = BL.getOverallSpendingHistory(date1, date2, depID);
            List<Object> months = new List<Object>();
            List<Object> secondmonths = new List<Object>();
            List<Object> years = new List<object>();
            List<decimal> datapoints2 = new List<decimal>();

            for (int i = 0; i < spendings.Count; i++)
            {
                DateTime myval = (DateTime)spendings[i].OrderDate;
                string month = myval.Month.ToString();
                string year = myval.Year.ToString();


                if (month == "1")
                {
                    if (!(secondmonths.Contains(month + year)))
                    {
                        months.Add("Jan" + " " + year.Substring(2, 2));
                        secondmonths.Add(month + year);
                        datapoints2.Add((decimal)spendings[i].TotalPrice);
                    }
                    else
                    {
                        datapoints2[datapoints2.Count - 1] += (decimal)spendings[i].TotalPrice;

                    }

                }

                else if (month == "2")
                {
                    if (!(secondmonths.Contains(month + year)))
                    {
                        months.Add("Feb" + " " + year.Substring(2, 2));
                        secondmonths.Add(month + year);
                        datapoints2.Add((decimal)spendings[i].TotalPrice);
                    }
                    else
                    {
                        datapoints2[datapoints2.Count - 1] += (decimal)spendings[i].TotalPrice;

                    }
                }
                else if (month == "3")
                {

                    if (!(secondmonths.Contains(month + year)))
                    {
                        months.Add("Mar" + " " + year.Substring(2, 2));
                        secondmonths.Add(month + year);
                        datapoints2.Add((decimal)spendings[i].TotalPrice);
                    }
                    else
                    {

                        datapoints2[datapoints2.Count - 1] += (decimal)spendings[i].TotalPrice;

                    }
                }

                else if (month == "4")
                {

                    if (!(secondmonths.Contains(month + year)))
                    {
                        months.Add("Apr" + " " + year.Substring(2, 2));
                        secondmonths.Add(month + year);
                        datapoints2.Add((decimal)spendings[i].TotalPrice);
                    }
                    else
                    {
                        datapoints2[datapoints2.Count - 1] += (decimal)spendings[i].TotalPrice;

                    }
                }

                else if (month == "5")
                {
                    if (!(secondmonths.Contains(month + year)))
                    {
                        months.Add("May" + " " + year.Substring(2, 2));
                        secondmonths.Add(month + year);
                        datapoints2.Add((decimal)spendings[i].TotalPrice);
                    }
                    else
                    {
                        datapoints2[datapoints2.Count - 1] += (decimal)spendings[i].TotalPrice;

                    }
                }

                else if (month == "6")
                {
                    if (!(secondmonths.Contains(month + year)))
                    {
                        months.Add("Jun" + " " + year.Substring(2, 2));
                        secondmonths.Add(month + year);
                        datapoints2.Add((decimal)spendings[i].TotalPrice);
                    }
                    else
                    {
                        datapoints2[datapoints2.Count - 1] += (decimal)spendings[i].TotalPrice;

                    }
                }

                else if (month == "7")
                {
                    if (!(secondmonths.Contains(month + year)))
                    {
                        months.Add("Jul" + " " + year.Substring(2, 2));
                        secondmonths.Add(month + year);
                        datapoints2.Add((decimal)spendings[i].TotalPrice);
                    }
                    else
                    {
                        datapoints2[datapoints2.Count - 1] += (decimal)spendings[i].TotalPrice;

                    }
                }

                else if (month == "8")
                {
                    if (!(secondmonths.Contains(month + year)))
                    {
                        months.Add("Aug" + " " + year.Substring(2, 2));
                        secondmonths.Add(month + year);
                        datapoints2.Add((decimal)spendings[i].TotalPrice);
                    }
                    else
                    {
                        datapoints2[datapoints2.Count - 1] += (decimal)spendings[i].TotalPrice;

                    }
                }

                else if (month == "9")
                {
                    if (!(secondmonths.Contains(month + year)))
                    {
                        months.Add("Sep" + " " + year.Substring(2, 2));
                        secondmonths.Add(month + year);
                        datapoints2.Add((decimal)spendings[i].TotalPrice);
                    }
                    else
                    {
                        datapoints2[datapoints2.Count - 1] += (decimal)spendings[i].TotalPrice;

                    }
                }
                else if (month == "10")
                {
                    if (!(secondmonths.Contains(month + year)))
                    {
                        months.Add("Oct" + " " + year.Substring(2, 2));
                        secondmonths.Add(month + year);
                        datapoints2.Add((decimal)spendings[i].TotalPrice);
                    }
                    else
                    {
                        datapoints2[datapoints2.Count - 1] += (decimal)spendings[i].TotalPrice;

                    }
                }
                else if (month == "11")
                {
                    if (!(secondmonths.Contains(month + year)))
                    {
                        months.Add("Nov" + " " + year.Substring(2, 2));
                        secondmonths.Add(month + year);
                        datapoints2.Add((decimal)spendings[i].TotalPrice);
                    }
                    else
                    {
                        datapoints2[datapoints2.Count - 1] += (decimal)spendings[i].TotalPrice;

                    }
                }

                else
                {
                    if (!(secondmonths.Contains(month + year)))
                    {
                        months.Add("Dec" + " " + year.Substring(2, 2));
                        secondmonths.Add(month + year);
                        datapoints2.Add((decimal)spendings[i].TotalPrice);
                    }
                    else
                    {
                        datapoints2[datapoints2.Count - 1] += (decimal)spendings[i].TotalPrice;

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
            else
            {

                dataZOOL = datapoints2;

            }

            datamonths = months;


        }
    }


    
}
