using System.Net.Mail;
using System.Net;
using Certifast.Console.Services.Interface;
using Certifast.Console.Models;

namespace Certifast.Console.Services
{
    public class EmailSender : IEmailSender
    {
        public void Send(string emailAdress, EmailData email)
        {
            File.AppendAllLines("C:\\Users\\avmd_\\OneDrive\\Área de Trabalho\\emails.txt", new List<string> { email.Body });
          //  return;
            var smtpClient = new SmtpClient("smtp.gmail.com")
            {
                Port = 587,
                Credentials = new NetworkCredential("teste@gmail.com", "alibvra GOUWO 123n ibs"),
                EnableSsl = true,
            };
            var mailMessage = new MailMessage
            {
                From = new MailAddress(emailAdress),
                Subject = email.Subject,
                Body = email.Body,
                IsBodyHtml = true,
            };
            mailMessage.To.Add(emailAdress);
            smtpClient.Send(mailMessage);
        }
    }
}
 
