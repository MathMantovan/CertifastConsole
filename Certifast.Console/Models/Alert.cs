using Certifast.Console.Services;
using OfficeOpenXml.Packaging.Ionic.Zlib;

namespace Certifast.Console.Models
{
    public static class AlertParser
    {
        public static List<Alert> GetAlertsFromCertificate(Certificate cert)
        {
            var alert = new List<Alert>();
            //1
            string emailAdress = cert.ClientEmail;
            string order = cert.Order;
            DateTime dateToSend = cert.ExpiringData.AddDays(-1);
            EmailData Email = EmailFormater.BuildEmail(cert, 1);
            bool Sent = false;
            alert.Add(new Alert(emailAdress, order, Email, Sent, dateToSend));
            //7
            order = cert.Order;
            dateToSend = cert.ExpiringData.AddDays(-7);
            Email = EmailFormater.BuildEmail(cert, 7);
            Sent = false;
            alert.Add(new Alert(emailAdress, order, Email, Sent, dateToSend));
            //30
            order = cert.Order;
            dateToSend = cert.ExpiringData.AddDays(-30);
            Email = EmailFormater.BuildEmail(cert, 30);
            Sent = false;
            alert.Add(new Alert(emailAdress, order, Email, Sent, dateToSend));

            return alert;
        }
    }
    public class Alert
    {
        public string EmailAdress { get; set; }
        public string Order { get; set; }
        public EmailData Email { get; set; }
        public bool Sent { get; set; }
        public DateTime DateToSend { get; set; }

        public Alert(string order, string order1, EmailData email, bool sent, DateTime dateToSend)
        {
            Order = order;
            Email = email;
            Sent = sent;
            DateToSend = dateToSend;
        }



    }
}
