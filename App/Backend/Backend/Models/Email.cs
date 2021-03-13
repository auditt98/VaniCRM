using MailKit.Net.Smtp;
using MimeKit;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;

namespace Backend.Models
{
    public class EmailRecipient
    {
        public string email { get; set; }
        public string name { get; set; }
    }

    public class Email
    {
        public string title { get; set; }
        public string content { get; set; }
        public IEnumerable<EmailRecipient> recipients { get; set; }

        public Email(string title, string content, List<EmailRecipient> recipients)
        {
            this.title = title;
            this.content = content;
            this.recipients = recipients;
        }

        public bool SendEmail()
        {
            var mailMessage = new MimeMessage();
            mailMessage.From.Add(new MailboxAddress("Vani CRM", "vanicrm.noreply@gmail.com"));
            foreach(var recipient in this.recipients)
            {
                mailMessage.To.Add(new MailboxAddress(recipient.email));
            }
            mailMessage.Subject = this.title;
            var bodyBuilder = new BodyBuilder();
            bodyBuilder.HtmlBody = $"<h1 style='font-family:'Google Sans'; font-size: 38px; color:#D93915;'>VANI CRM</h1><h1>{this.title}</h1><br/><br/><p>{this.content}</p>";
            bodyBuilder.TextBody = "This is some plain text";

            mailMessage.Body = bodyBuilder.ToMessageBody();
            using (var smtpClient = new SmtpClient())
            {
                smtpClient.Connect("smtp.gmail.com", 465, true);
                smtpClient.Authenticate("vanicrm.noreply@gmail.com", "TVA180501");
                smtpClient.Send(mailMessage);
                smtpClient.Disconnect(true);
            }
            return true;
        }
    }
}