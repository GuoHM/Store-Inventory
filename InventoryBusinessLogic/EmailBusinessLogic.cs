using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;
using InventoryBusinessLogic.Entity;

namespace InventoryBusinessLogic
{
    public class EmailBusinessLogic

    {
        Inventory inventory = new Inventory();
        private static string FROM_ADDRESS = "team3adproject@outlook.com";
        private static string CREDENTIALS = "ADproject";
        private static int PORT = 587;
        private static string HOST = "smtp-mail.outlook.com";
        private static bool ENABLESSL = true;
        private MailMessage mail;
        private SmtpClient client;

        public EmailBusinessLogic()
        {
            this.mail = new MailMessage();
            this.mail.From = new MailAddress(FROM_ADDRESS, "Email head", System.Text.Encoding.UTF8);
            this.mail.SubjectEncoding = System.Text.Encoding.UTF8;
            this.mail.BodyEncoding = System.Text.Encoding.UTF8;
            this.mail.IsBodyHtml = true;
            this.mail.Priority = MailPriority.High;
            this.client = new SmtpClient();
            this.client.Credentials = new System.Net.NetworkCredential(FROM_ADDRESS, CREDENTIALS);
            this.client.Port = PORT;
            this.client.Host = HOST;
            this.client.EnableSsl = ENABLESSL;
        }

        /// <summary>
        /// Send email to a list of user
        /// </summary>
        /// <param name="title">Email Title</param>
        /// <param name="content">Email Content</param>
        /// <param name="toAddress">To Email address list</param>
        public void SendEmail(string title, string content, List<string> toAddress)
        {
            foreach (string email in toAddress)
            {
                this.mail.To.Add(email);
            }
            this.mail.Subject = title;
            this.mail.Body = content;
            this.client.Send(this.mail);
        }

        //1.郭浩明
        public string SendPurchaseOrderNotification(string orderIDstring)
        {
            int orderID = Convert.ToInt32(orderIDstring);
            PurchaseOrder purchaseOrder = inventory.PurchaseOrder.Where(x => x.PurchaseOrderID == orderID).First();
            List<PurchaseItem> purchaseItems = inventory.PurchaseItem.Where(x => x.PurchaseOrderID == orderID).ToList();
            string MailBody = "<p> Dear "+purchaseOrder.Supplier.ContactName+",</p>";
            MailBody += "<p>Logic University has sent a purchase order by "+purchaseOrder.AspNetUsers.Name+". <br> <b>OrderID: " + purchaseOrder.PurchaseOrderID+"<br> Expected Date:  "+ purchaseOrder.ExpectedDate+ ".</b><br> Please find details below. <br>";
            MailBody += "<div>";
            MailBody += "<table border='1'>";
            MailBody += "<tr>";
            MailBody += "<td>Item No</td><td>Description</td><td>Quantity</td><td>Price</td><td>Amount</td>";
            MailBody += "</tr>";

            double? amount = 0;

            foreach (PurchaseItem item in purchaseItems)

            {
                
                MailBody += "<tr>";
                MailBody += "<td>"+item.ItemID+"</td><td>"+item.Catalogue.Description+"</td><td>"+item.Quantity+"</td><td>"+string.Format("{0:0.00}",item.Catalogue.Price)+"</td><td>"+ string.Format("{0:0.00}", item.Catalogue.Price * item.Quantity )+ "</td>";
                MailBody += "</tr>";
                amount += item.Catalogue.Price * item.Quantity;
            }
            MailBody += "<tr>";
            MailBody += "<td></td><td>Total Cost</td><td></td><td></td><td>"+ string.Format("{0:0.00}", amount) +"</td>";
            MailBody += "</tr>";




            MailBody += "</table>";
            MailBody += "</div>";
            MailBody += "<p> Kindly acknowledge the purchase order.<br> This is a system generated email. </ p>";
            
            return MailBody;
        }
        //2.郭浩明
        public string SendRequestNotification(string  userID)
        {
        
            AspNetUsers employee = inventory.AspNetUsers.Where(x => x.Id == userID).First();
            AspNetUsers departmentHead = inventory.AspNetUsers.Where(x => x.Department == employee.Department).First();
            string MailBody = "<p> Dear " + departmentHead.Name + ",</p>";
            MailBody += "<p> Order request from "+employee.Name+" is awaiting your action.< br> This is a system generated email.<br>Thank you </ p>";

            return MailBody;
        }
        //3.郭浩明
        public string NewVoucherNotification(string adjustmentIDstring ,string userID )
        {
            int adjustmentID = Convert.ToInt32(adjustmentIDstring);
             
            AspNetUsers employee = inventory.AspNetUsers.Where(x => x.Id == userID).First();
             Adjustment adjustment = inventory.Adjustment.Where(x => x.AdjustmentID == adjustmentID).First();
            AspNetUsers departmentHead = inventory.AspNetUsers.Where(x => x.Department == employee.Department).First();
            string MailBody = "<p> Dear " + departmentHead.Name + ",</p>";
            MailBody += "<p> Adjustment voucher request: "+adjustment.AdjustmentID+" from " + employee.Name + " is awaiting your action.< br> This is a system generated email.<br>Thank you </ p>";

            return MailBody;
        }
        //4.以后再说 Padma
        public string LowInventoryNotification()
        {
            return null;
        }
        //5.RuiXiang
        public string ChangePointNotification(string RepID,string clerkID,string oldPoint,string newPoint)
        {
            
            AspNetUsers departmentRep = inventory.AspNetUsers.Where(x => x.Id == RepID).First();
            AspNetUsers clerk= inventory.AspNetUsers.Where(x => x.Id == clerkID).First();
            string MailBody = "<p> Dear " + clerk.Name + ",</p>";
            MailBody += "<p> "+departmentRep.Name+" from "+ departmentRep.Department+" has requested to change collection point from "+oldPoint +" to "+newPoint+".< br > This is a system generated email.<br>Thank you </ p>";

            return MailBody;
        }
        //6. Ronith
        public  string ChangeDeptHeadNotification(string employeeID)
        {
            
            AspNetUsers employee = inventory.AspNetUsers.Where(x => x.Id == employeeID).First();
            Department department = inventory.Department.Where(x => x.DepartmentID == employee.DepartmentID).First();
            string MailBody = "<p> Dear " + employee.Name + ",</p>";
            MailBody += "<p> The department head is authorising you from "+ department.DepartmentHeadStartDate+" to "+department.DepartmentHeadEndDate+" for department head duties.< br > This is a system generated email.< br > Thank you </ p > ";
            return MailBody;
        }
        //7.Ronith
        public string ChangeDeptRepNotification(string headID,string oldRep, string clerkID)
        {
            AspNetUsers departmentHead = inventory.AspNetUsers.Where(x => x.Id == headID).First();
            AspNetUsers departmentRep = inventory.AspNetUsers.Where(x => x.DepartmentID == departmentHead.DepartmentID).First();
            AspNetUsers clerk = inventory.AspNetUsers.Where(x => x.Id == clerkID).First();
            string MailBody = "<p> Dear " + clerk.Name + ",</p>";
            MailBody += "<p> " + departmentHead.Name+" has changed their department representative from "+oldRep+" to "+departmentRep.Name + ".< br > This is a system generated email.<br>Thank you </ p>";

            return MailBody;
        }
        //8.Padma
        public string ApproveOrRejectNotification(int requestID,string approveOrReject)
        {

            Request request = inventory.Request.Where(x => x.RequestID == requestID).First();
            AspNetUsers employee = request.AspNetUsers;
            string Mailbody = "<p> Dear " + employee.Name + ",</p>";
            Mailbody += "<p> Your request has been " + approveOrReject + ".< br > This is a system generated email.<br>Thank you </ p>";
            return Mailbody;
        }
        //9.Padma
        public string ReadyForCollectionPoint(string departmentId)
        {
            Department department = inventory.Department.Where(x => x.DepartmentID == departmentId).First();
            AspNetUsers departmentRep = inventory.AspNetUsers.Where(x => x.DepartmentID == departmentId).First();
            string Mailbody = "<p> Dear " + departmentRep.Name + ",</p>";
            Mailbody += "<p> Your " +department.DepartmentName+" items are ready for collection.< br > This is a system generated email.<br>Thank you </ p>";
            return Mailbody;
        }





        public class SelectedList
        {
            public string itemID { get; set; }

            public string description { get; set; }

            public string quantity { get; set; }

            public string totalPrice { get; set; }

            public string supplier { get; set; }
        }

        public class confirmClass
        {
            public List<SelectedList> tablelist { get; set; }

            public string supplierAddress { get; set; }

            public string delieverTo { get; set; }

            public string attentionTo { get; set; }

            public string dateToDeliver { get; set; }

        }

    }
}