using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;

namespace Email
{
    public class EmailService : IEmailService
    {
        public void SendEmailToUser(String userEmail, String subject, String header, String body)
        {
            String senderEmail = "codycofysoft@gmail.com";
            String senderPassword = "qijsbnmhagualwar";
            MailMessage message = new MailMessage();
            message.From = new MailAddress(senderEmail);
            message.Subject = subject;
            message.To.Add(new MailAddress(userEmail));
            message.IsBodyHtml = true;
            message.Body = "<html><body><h1>"+header+"</h1>"+body+"</body></html>";

            var smtpClient = new SmtpClient("smtp.gmail.com")
            {
                Port = 587,
                Credentials = new NetworkCredential(senderEmail, senderPassword),
                EnableSsl = true
            };
            smtpClient.Send(message);
        }
    }
}
