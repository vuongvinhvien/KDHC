using Microsoft.AspNet.Identity;
using System;
using System.Net;
using System.Threading.Tasks;
using System.Configuration;
using System.Net.Mail;

namespace Store.Sevices.Services
{
    public class SendMailSevices : IIdentityMessageService
    {

        public Task SendAsync(IdentityMessage message)
        {

            var mail = new MailMessage();
      
            mail.From = new MailAddress(ConfigurationManager.AppSettings["sendGridFrom"]);

            mail.To.Add(message.Destination);



            SmtpClient smtp = new SmtpClient
            {
                Host = ConfigurationManager.AppSettings["sendGridSmtpServer"],
                Port = Convert.ToInt32(ConfigurationManager.AppSettings["sendGridPort"]),
                DeliveryMethod = SmtpDeliveryMethod.Network,
                UseDefaultCredentials = false,
                EnableSsl = Convert.ToBoolean(ConfigurationManager.AppSettings["EnableSsl"]),
                Credentials = new NetworkCredential(ConfigurationManager.AppSettings["sendGridUser"], ConfigurationManager.AppSettings["sendGridPassword"]),
                Timeout = 30000,
            };
            mail.Subject = message.Subject;
            mail.Body = message.Body;
            mail.IsBodyHtml = true;

            smtp.Send(mail);
            return Task.FromResult(0);


        }
    }
}
