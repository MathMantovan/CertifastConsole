using Certifast.Console.Models;
using Certifast.Console.Services.Exception;

namespace Certifast.Console.Services
{
    public static class AlertParser
    {
        public static List<Alert> GetAlertsFromCertificate(Certificate cert)
        {
            HasCertificateInfo(cert);

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

        private static void HasCertificateInfo(Certificate cert)
        {
            if (cert.Order == "")
                throw new CertificateException("Certificado sem numero de pedido");
            if (cert.ExpiringData == null )
                throw new CertificateException(" Certificado sem data de renovação");
            if (cert.ExpiringData <= DateTime.Today)
                throw new CertificateException(" Certificado tem data de vencimento anterior ou igual a hoje");
            if (cert.ClientEmail == "")
                throw new CertificateException("Certificado sem Email para contato");
        }
    }
}
