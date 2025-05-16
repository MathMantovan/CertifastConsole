
using System.Collections.Generic;
using Certifast.Console.Models;
using Certifast.Console.Services.Interface;

namespace Certifast.Console.Services
{
    class NoSqlDataBase : INoSqlDataBase
    {
        public string Path = "C:\\Users\\avmd_\\OneDrive\\Área de Trabalho\\Matheus\\Programação\\Bd";
        public static List<AlertDocument> Alerts = new List<AlertDocument>();

        public List<Alert> GetAlerts(DateTime today)
        {
            return Alerts
                .Where(x => x.Alert.DateToSend == today && !x.Alert.Sent)
                .Select(x => x.Alert)
                .ToList();
        }

        public void Store(Alert alert)
        {
            AlertDocument AlertDb = new AlertDocument(alert);
            Alerts.Add(AlertDb);
        }
    }

}

