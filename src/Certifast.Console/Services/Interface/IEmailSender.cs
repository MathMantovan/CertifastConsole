using Certifast.Console.Models;

namespace Certifast.Console.Services.Interface
{
    public interface IEmailSender
    {
        void Send(string emailAdress, EmailData email);
    }    
}