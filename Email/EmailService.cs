using Security;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace Email
{
    public class EmailService : IEmailService
    {
        public void SendEmailToUser(String userEmail, String subject, String header, String body)
        {
            Credentials credentials = ParseCredentials();
            String senderEmail = credentials.Email;
            String senderPassword = credentials.Password;

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

        private Credentials ParseCredentials()
        {
            byte[] credentialsFile = File.ReadAllBytes("Credentials.crd");
            byte[] encryptionKey = Encoding.UTF8.GetBytes("ArgiopeAurantia");
            MemoryStream memoryStream = 
                new MemoryStream(EncryptionService.AES_Decrypt(credentialsFile, encryptionKey));
            XmlSerializer xmlSerializer = new XmlSerializer(typeof(Credentials));
            return xmlSerializer.Deserialize(memoryStream) as Credentials;

        }

        private void MakeNewCredentials(string email, string password, string key)
        {
            XmlSerializer xmlSerializer = new XmlSerializer(typeof(Credentials));
            MemoryStream memoryStream = new MemoryStream();
            xmlSerializer.Serialize(memoryStream, new Credentials() { Email = email, Password = password });
            byte[] encrypted = EncryptionService.AES_Encrypt(memoryStream.ToArray(), Encoding.UTF8.GetBytes(key)); //This is the key
            File.WriteAllBytes("Credentials.crd", encrypted);
        }

    }

}
