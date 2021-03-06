﻿using Backend.Domain;
using MailKit.Net.Smtp;
using MimeKit;
using System;
using System.Collections.Generic;
using System.Linq;


using System.Web;

namespace Backend.Extensions
{
    /// <summary>
    /// Use SendBatch() when sending to multiple people. IMPORTANT!
    /// </summary>

    public class EmailManager
    {
        public string Title { get; set; }
        public string Content { get; set; }
        public List<string> Recipients { get; set; }
        public bool isSent { get; set; }
        public string error { get; set; }
        public int NumberSent { get; set; }
        public int BatchSize { get; set; }

        public EmailManager()
        {
            Title = "";
            Content = "";
            Recipients = new List<string>();
        }

        public EmailManager(string title, string content, List<string> recipients)
        {
            this.Title = title;
            this.Content = content;
            this.Recipients = recipients;
        }

        public EmailManager SendEmail()
        {
            var mailMessage = new MimeMessage();
            mailMessage.From.Add(new MailboxAddress("Vani CRM", "vanicrm.noreply@gmail.com"));
            try
            {
                foreach (var recipient in Recipients)
                {
                    mailMessage.To.Add(new MailboxAddress(recipient));
                    mailMessage.Subject = Title;

                    var bodyBuilder = new BodyBuilder();
                    bodyBuilder.HtmlBody = $"<h1 style='color: #D93915'>CRM</h1><h2>{Title}</h2><br/><p>{Content}</p>";
                    bodyBuilder.TextBody = $"";
                    mailMessage.Body = bodyBuilder.ToMessageBody();
                    using (var smtpClient = new SmtpClient())
                    {
                        smtpClient.Connect("smtp.gmail.com", 465, true);
                        smtpClient.Authenticate("vanicrm.noreply@gmail.com", "TVA180501");
                        smtpClient.Send(mailMessage);
                        smtpClient.Disconnect(true);

                    }
                }
                this.isSent = true;
                return this;
            }
            catch (Exception e)
            {
                this.isSent = false;
                this.error = e.Message;
                return this;
            }
        }

        public EmailManager SendBatch()
        {
            var mailMessage = new MimeMessage();
            mailMessage.From.Add(new MailboxAddress("Vani CRM", "vanicrm.noreply@gmail.com"));
            mailMessage.To.Add(new GroupAddress("undisclosed-recipients"));
            
            using (var smtpClient = new SmtpClient())
            {
                smtpClient.Connect("smtp.gmail.com", 465, true);
                smtpClient.Authenticate("vanicrm.noreply@gmail.com", "TVA180501");
                var recipientList = new List<MailboxAddress>();
                foreach(var recipient in Recipients)
                {
                    recipientList.Add(MailboxAddress.Parse(recipient));
                }
                mailMessage.Subject = Title;
                var bodyBuilder = new BodyBuilder();
                bodyBuilder.HtmlBody = $"<h1 style='color: #D93915'>CRM</h1><h2>{Title}</h2><br/><p>{Content}</p>";
                bodyBuilder.TextBody = $"";
                mailMessage.Body = bodyBuilder.ToMessageBody();
                try
                {
                    smtpClient.Send(mailMessage, new MailboxAddress("Vani CRM", "vanicrm.noreply@gmail.com"), recipientList);
                    this.isSent = true;
                }
                catch(Exception e)
                {
                    this.isSent = false;
                    this.error = e.Message;
                }
            }
            return this;

        }
    }
}