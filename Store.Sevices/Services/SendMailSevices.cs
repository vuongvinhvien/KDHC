using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

using System.Net.Mail;

namespace Store.Sevices.Services
{
    public class SendMailSevices : IIdentityMessageService
    {

        public Task SendAsync(IdentityMessage message)
        {
            var mail = new MailMessage();
            var smtpServer = new SmtpClient(ConfigurationManager.AppSettings["sendGridSmtpServer"]);
            mail.From = new MailAddress(ConfigurationManager.AppSettings["sendGridFrom"]);

            mail.To.Add("13520748@gm.uit.edu.vn");
            smtpServer.Port = Convert.ToInt32(ConfigurationManager.AppSettings["sendGridPort"]);
            smtpServer.Credentials = new NetworkCredential(ConfigurationManager.AppSettings["sendGridUser"], ConfigurationManager.AppSettings["sendGridPassword"]);
            smtpServer.EnableSsl = Convert.ToBoolean(ConfigurationManager.AppSettings["EnableSsl"]);

            mail.Subject = "Subject here";
            mail.Body = "Body message here";
            mail.IsBodyHtml = true;

            smtpServer.Send(mail);
            return Task.FromResult(0);


        }
    }
}
