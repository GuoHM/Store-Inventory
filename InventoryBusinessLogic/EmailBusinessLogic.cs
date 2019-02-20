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

        //1.郭浩明 Test Successful
        public string SendPurchaseOrderNotification(int orderID)
        {
            
            PurchaseOrder purchaseOrder = inventory.PurchaseOrder.Where(x => x.PurchaseOrderID == orderID).First();
            List<PurchaseItem> purchaseItems = inventory.PurchaseItem.Where(x => x.PurchaseOrderID == orderID).ToList();
            string MailBody = "<p> Dear "+purchaseOrder.Supplier.ContactName+",</p>";
            MailBody += "<p>Logic University has sent a purchase order by "+purchaseOrder.AspNetUsers.UserName+". <br> <b>OrderID: " + purchaseOrder.PurchaseOrderID+"<br> Expected Date:  "+ purchaseOrder.ExpectedDate+ ".</b><br> Please find details below. <br>";
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
        //2.郭浩明 Test Successful
        //EmailBusinessLogic emailBusinessLogic = new EmailBusinessLogic();
        //string content = emailBusinessLogic.SendRequestNotification(username);
        //List<string> toAddress = new List<string>();
        //toAddress.Add("wangxiaoxiaoqiang@gmail.com");
        // emailBusinessLogic.SendEmail("Team3", content, toAddress);
        public string SendRequestNotification(string  userID)
        {
        
            AspNetUsers employee = inventory.AspNetUsers.Where(x => x.Id == userID).First();
            AspNetUsers departmentHead = inventory.AspNetUsers.Where(x => x.DepartmentID == employee.DepartmentID).First();
            string MailBody = "<p> Dear " + departmentHead.UserName + ",</p>";
            MailBody += "<p> Order request from "+employee.UserName+" is awaiting your action.<br> This is a system generated email.<br>Thank you </ p>";

            return MailBody;
        }
        //3.郭浩明 Test Successful
        //EmailBusinessLogic emailBusinessLogic = new EmailBusinessLogic();
        //string content = emailBusinessLogic.NewVoucherNotification(adjustment.AdjustmentID, adjustment.UserID);
        //List<string> toAddress = new List<string>();
        //toAddress.Add("wangxiaoxiaoqiang@gmail.com");
        // emailBusinessLogic.SendEmail("Team3", content, toAddress);
        public string NewVoucherNotification(int adjustmentID ,string userID )
        {
            
             
            AspNetUsers employee = inventory.AspNetUsers.Where(x => x.Id == userID).First();
             Adjustment adjustment = inventory.Adjustment.Where(x => x.AdjustmentID == adjustmentID).First();
            AspNetUsers departmentHead = inventory.AspNetUsers.Where(x => x.DepartmentID == employee.DepartmentID).First();
            string MailBody = "<p> Dear " + departmentHead.UserName + ",</p>";
            MailBody += "<p> Adjustment voucher request: "+adjustment.AdjustmentID+" from " + employee.UserName + " is awaiting your action.<br> This is a system generated email.<br>Thank you </ p>";

            return MailBody;
        }
        //4.Padma Later
        public string LowStockNotification()
        {
            inventory.Configuration.ProxyCreationEnabled = false;
            List<Catalogue>  lowstockItems = inventory.Catalogue.Where(x => x.Quantity <= x.ReorderLevel ).ToList();
            
            string MailBody = "<p> Dear Store Clerk,</p>";
            MailBody += "<p>Items below are falling below reorder level. </p>";
            MailBody += "<div>";
            MailBody += "<table border='1'>";
            MailBody += "<tr>";
            MailBody += "<td>Item No</td><td>Description</td><td>Category</td><td>Quantity</td><td>Measure Unit</td><td>Reorder Quantity</td><td>Bin</td>";
            MailBody += "</tr>";

            foreach (Catalogue item in lowstockItems)

            {

                MailBody += "<tr>";
                MailBody += "<td>" + item.ItemID + "</td><td>" + item.Description + "</td><td>" + item.Category + "</td><td>" + item.Quantity + "</td><td>" + item.MeasureUnit + "</td><td>" + item.ReorderQuantity + "</td><td>" + item.BinNumber+ "</td>";
                MailBody += "</tr>";
                
            }
            
            MailBody += "</table>";
            MailBody += "</div>";
            MailBody += "<p> This is a system generated email.<br>Thank you. </ p>";

            return MailBody;
        }

        //5.RuiXiang Test Successful
        //EmailBusinessLogic emailBusinessLogic = new EmailBusinessLogic();
        //string content = emailBusinessLogic.ChangePointNotification(User.Identity.Name, CollectionPoint);

        //List<string> toAddress = new List<string>();
        //toAddress.Add("wangxiaoxiaoqiang@gmail.com");
        //    emailBusinessLogic.SendEmail("Team3", content, toAddress);
        public string ChangePointNotification(string RepName,string newPoint)
        {
            
            AspNetUsers departmentRep = inventory.AspNetUsers.Where(x => x.Name == RepName).First();
            Department department = inventory.Department.Where(x => x.DepartmentID == departmentRep.DepartmentID).First();

 
            string MailBody = "<p> Dear Store Clerk,</p>";
            MailBody += "<p> "+departmentRep.UserName+" from "+ department.DepartmentName+" has requested to change collection point to "+newPoint+".<br> This is a system generated email.<br>Thank you </ p>";

            return MailBody;
        }
        //6. Ronith Test Successful
        //EmailBusinessLogic emailBusinessLogic = new EmailBusinessLogic();
        //string content = emailBusinessLogic.ChangeDeptHeadNotification(dropdown1);

        //List<string> toAddress = new List<string>();
        //toAddress.Add("wangxiaoxiaoqiang@gmail.com");
        //    emailBusinessLogic.SendEmail("Team3", content, toAddress);
        public string ChangeDeptHeadNotification(string employeeID )
        {
            
            AspNetUsers employee = inventory.AspNetUsers.Where(x => x.Id == employeeID).First();
            Department department = inventory.Department.Where(x => x.DepartmentID == employee.DepartmentID).First();
            string MailBody = "<p> Dear " + employee.UserName + ",</p>";
            MailBody += "<p> The department head is authorising you from "+ department.DepartmentHeadStartDate+" to "+department.DepartmentHeadEndDate+" for department head duties.<br> This is a system generated email.<br> Thank you </ p > ";
            return MailBody;
        }
        //7.Ronith Test Successful
        //EmailBusinessLogic emailBusinessLogic = new EmailBusinessLogic();
        //string content = emailBusinessLogic.ChangeDeptRepNotification(dropdown1);

        //List<string> toAddress = new List<string>();
        //toAddress.Add("wangxiaoxiaoqiang@gmail.com");
        //    emailBusinessLogic.SendEmail("Team3", content, toAddress);
        public string ChangeDeptRepNotification(string repID)
        {
            AspNetUsers departmentRep = inventory.AspNetUsers.Where(x => x.Id == repID).First();
            AspNetUsers departmentHead = inventory.AspNetUsers.Where(x => x.DepartmentID == departmentRep.DepartmentID).First();

            string MailBody = "<p> Dear Store Clerk ,</p>";
            MailBody += "<p> " + departmentHead.UserName + " has changed their department representative to " + departmentRep.UserName + ".<br> This is a system generated email.<br>Thank you </ p>";

            return MailBody;
        }

            //8.Padma test successful
            //var item1 = list[0];
            //EmailBusinessLogic emailBusinessLogic = new EmailBusinessLogic();
            //int requestID = Convert.ToInt32(item1.orderId);
            //string content = emailBusinessLogic.ApproveOrRejectNotification(requestID);

            //List<string> toAddress = new List<string>();
            //toAddress.Add("wangxiaoxiaoqiang@gmail.com");
            //emailBusinessLogic.SendEmail("Team3", content, toAddress);
        
        public string ApproveOrRejectNotification(int requestID)
        {

            Request request = inventory.Request.Where(x => x.RequestID == requestID).First();
            AspNetUsers employee = request.AspNetUsers;
            string Mailbody = "<p> Dear " + employee.UserName + ",</p>";
            Mailbody += "<p> Your request has been " + request.RequestStatus;
            if (request.Remarks != null && request.Remarks != "")
            {
                Mailbody += "<p> Reason: " + request.Remarks;
            }

            Mailbody += ".<br> This is a system generated email.<br>Thank you </ p>";
            return Mailbody;
        }
        //9.Padma test successful
        public string ReadyForCollectionPoint(string departmentId)
        {
            Department department = inventory.Department.Where(x => x.DepartmentID == departmentId).First();
            AspNetUsers departmentRep = inventory.AspNetUsers.Where(x => x.DepartmentID == departmentId).First();
            string Mailbody = "<p> Dear " + departmentRep.UserName + ",</p>";
            Mailbody += "<p> Your " +department.DepartmentName+" items are ready for collection.<br> This is a system generated email.<br>Thank you </ p>";
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