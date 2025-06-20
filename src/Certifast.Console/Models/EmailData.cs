namespace Certifast.Console.Models
{
    public class EmailData
    {
        public Certificate Certificate { get; set; }
        public string Body { get; set; }
        public string Subject { get; set; }
        public int Expiring { get; set; }  
    }
}
