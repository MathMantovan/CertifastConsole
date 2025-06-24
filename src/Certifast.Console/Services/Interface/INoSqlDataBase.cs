using Certifast.Console.Models;

namespace Certifast.Console.Services.Interface
{
    public interface INoSqlDataBase
    {
        List<Alert> GetAlerts(DateTime today);
        void Store(Alert alert);
        public void Update(List<Alert> AlertsUpdated);

    }
}
