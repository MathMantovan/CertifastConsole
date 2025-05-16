using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Certifast.Console.Models;

namespace Certifast.Console.Services
{
    public static class AlertParser
    {
        public static List<Alert> GetAlertsFromCertificate(Certificate cert)
        {
            if (cert.Order == null)
                throw new Exception("");

            var alert = new List<Alert>();
            //1
            string emailAdress = cert.ClientEmail;
            string order = cert.Order;
            DateTime dateToSend = cert.ExpiringData.AddDays(-1);
            EmailData Email = EmailFormater.BuildEmail(cert, 1);
            bool Sent = false;
            alert.Add(new Alert(order, emailAdress, Email, Sent, dateToSend));
            //7
            order = cert.Order;
            dateToSend = cert.ExpiringData.AddDays(-7);
            Email = EmailFormater.BuildEmail(cert, 7);
            Sent = false;
            alert.Add(new Alert(order, emailAdress, Email, Sent, dateToSend));
            //30
            order = cert.Order;
            dateToSend = cert.ExpiringData.AddDays(-30);
            Email = EmailFormater.BuildEmail(cert, 30);
            Sent = false;
            alert.Add(new Alert(order, emailAdress, Email, Sent, dateToSend));

            return alert;
        }
    }
}
