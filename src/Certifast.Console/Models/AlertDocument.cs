
namespace Certifast.Console.Models
{
    public class AlertDocument
    {
        public string Id { get; set; }
        public Alert Alert { get; set; }

        public AlertDocument(Alert alert)
        {
            Id = alert.DateToSend + "_" + alert.Order;
            Alert = alert;
        }
    }
}
