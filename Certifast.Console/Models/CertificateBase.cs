namespace Certifast.Console.Models
{
    public class CertificateBase
    {
        public string Cell { get; set; }
        public string ClientEmail { get; set; }
        public string ClientName { get; set; }
        public string Cnpj { get; set; }
        public string CompanyName { get; set; }
        public string Cpf { get; set; }
        public DateTime ExpiringData { get; set; }
        public string Order { get; set; }
        public string ResponseAgent { get; set; }
        public string Type { get; set; }
    }
}