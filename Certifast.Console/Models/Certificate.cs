using System.Security.Cryptography;
using Certifast.Console.Models;
using Certifast.Console.Services;

namespace Certifast.Console.Models
{
    public class Certificate
    {


        public string Order { get; set; }
        public DateTime ExpiringData { get; set; }
        public string ClientName { get; set; }
        public string Type { get; set; }
        public string ClientEmail { get; set; }
        public string Cell { get; set; }
        public string ResponseAgent { get; set; }
        public string Cpf { get; set; }
        public string Cnpj { get; set; }
        public string CompanyName { get; set; }

        public Certificate(string order, DateTime expiringData, string clientName, string clientEmail, string cell, string type, string responseAgent, string cpf, string cnpj, string companyName)
        {
            Order = order;
            ExpiringData = expiringData;
            ClientName = clientName;
            Type = type;
            ClientEmail = clientEmail;
            Cell = cell;
            ResponseAgent = responseAgent;
            Cpf = cpf;
            Cnpj = cnpj;
            CompanyName = companyName;
        }

        public bool HasCnpj()
        {
            return Cnpj == null;

        }
        public int GetDaysToExpire()
        {
            int expiring = 0;
            //1, 7, 30
            if (DateTime.Today.AddDays(1) == ExpiringData.Date)
            {
                // Expira amanhã
                expiring = (DateTime.Today - ExpiringData.Date).Days;
            }
            else if (DateTime.Today.AddDays(7) == ExpiringData.Date)
            {
                // expira em 7
                expiring = (DateTime.Today - ExpiringData.Date).Days;
            }
            else if (DateTime.Today.AddMonths(1) == ExpiringData.Date)
            {
                //expira em 30
                expiring = (DateTime.Today - ExpiringData.Date).Days;
            }
            return expiring;
        }

    }
}




