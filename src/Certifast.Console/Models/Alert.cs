using LiteDB;

namespace Certifast.Console.Models
{
    public class Alert
    {
        [BsonId]
        public ObjectId Id { get; set; } = ObjectId.NewObjectId();

        public string EmailAdress { get; set; }
        public string Order { get; set; }
        public EmailData Email { get; set; }
        public bool Sent { get; set; }
        public DateTime DateToSend { get; set; }

        public Alert(string order, string emailAdress, EmailData email, bool sent, DateTime dateToSend)
        {
            Id = ObjectId.NewObjectId();
            Order = order;
            EmailAdress = emailAdress;
            Email = email;
            Sent = sent;
            DateToSend = dateToSend;
        }
    }
}
