using Certifast.Console.Models;

namespace Certifast.Console.Services
{    
    public class EmailFormater
    {

        public static EmailData BuildEmail(Certificate certificate)
        {
            EmailData email = new EmailData();
            email.Certificate = certificate;
            email.Expiring = certificate.GetDaysToExpire();
            email.Body = FormatBody(certificate, email.Expiring);
            email.Subject = FormatSubject(certificate, email.Expiring);

            return email;
        }
        public static EmailData BuildEmail(Certificate certificate, int daysToExpire)
        {
            EmailData email = new EmailData();
            email.Certificate = certificate;
            email.Expiring = daysToExpire;
            email.Body = FormatBody(certificate, email.Expiring);
            email.Subject = FormatSubject(certificate, email.Expiring);

            return email;
        }

        public static string FormatBody(Certificate certificate, int expiringDays)
        {
            string body = null;

            var qualquer = certificate.HasCnpj()
                ? $"a empresa de Cnpj {certificate.Cnpj}"
                : $"ao Cpf {certificate.Cpf}";

            body = $@"
                Olá {certificate.ClientName},

                Este é um lembrete de que seu certificado digital do tipo **{certificate.Type}** referente {qualquer} irá vencer em {expiringDays} dias.

                
                Recomendamos que você inicie o processo de renovação o quanto antes para evitar interrupções em suas atividades digitais.

                Caso já tenha realizado a renovação, por favor desconsidere esta mensagem.

                Atenciosamente,  
                Equipe Certifast
                ";

            return body;
        }

        public static string FormatSubject(Certificate certificate, int expiringDays)
        {
            string Subject = null;
            if (expiringDays >= 1 && expiringDays < 6)
            {
                Subject = "Seu certificado digital vai vencer AMANHÃ!!!";
            }
            else if (expiringDays > 6 && expiringDays < 10)
            {
                Subject = "Seu certificado digital vai vencer em até 7 dias!";
            }
            else if (expiringDays > 29)
            {
                Subject = "Seu certificado digital vai vencer em até 1 mês!";
            }
            return Subject;
        }

    }


}
