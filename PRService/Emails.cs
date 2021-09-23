using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;
using System.Net.Mail;
using System.Configuration;

namespace PRService
{
    class Emails
    {
        string FromEmail;
        string ToEmail;
        string Pass;
        string Title;
        string Body;

        public Emails(string _ToEmail, string _title, string _body)
        {
            FromEmail = ConfigurationManager.AppSettings["FromEmail"];
            Pass = ConfigurationManager.AppSettings["EmailPass"];
            ToEmail = _ToEmail;
            Title = _title;
            Body = _body;
        }
        public bool sendMail()
        {
            bool isSend = false;
            using (MailMessage mail = new MailMessage())
            {
                mail.From = new MailAddress(FromEmail);
                mail.To.Add(ToEmail);
                mail.Subject = Title;
                mail.Body = Body;
                mail.IsBodyHtml = true;
                //mail.Attachments.Add(new Attachment("C:\\file.zip"));

                using (SmtpClient smtp = new SmtpClient("smtp.gmail.com", 587))
                {
                    smtp.Credentials = new NetworkCredential(FromEmail, Pass);
                    smtp.EnableSsl = true;
                    try
                    {
                        smtp.Send(mail);
                    }

                    catch (Exception ex)
                    {
                        throw ex;
                    }
                }
            }
            isSend = true;
            return isSend;
        }
    }
}
