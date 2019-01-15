using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;

namespace InventoryBusinessLogic
{
    public class EmailBusinessLogic
    {
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
            foreach(string email in toAddress)
            {
                this.mail.To.Add(email);
            }
            this.mail.Subject = title;
            this.mail.Body = content;
            this.client.Send(this.mail);
        }

    }
}
