
using Certifast.Console.Models;
using Certifast.Console.Services.Interface;
using LiteDB;
using System.Collections.Generic;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace Certifast.Console.Services
{
   public class NoSqlDataBase : INoSqlDataBase
    {
        private const string DbPath = "Certifast.db";
        //public string Path = "C:\\Users\\avmd_\\OneDrive\\Área de Trabalho\\Matheus\\Programação\\Bd";
        //public static List<Alert> Alerts = new List<Alert>();

        public List<Alert> GetAlerts(DateTime today)
        {
            using var db = new LiteDatabase(DbPath);
            var AlertDb = db.GetCollection<Alert>("Alerts");
            return AlertDb.Find(x => x.DateToSend == today).ToList();
            //return Alerts
            //    .Where(x => x.Alert.DateToSend == today && !x.Alert.Sent)
            //    .Select(x => x.Alert)
            //    .ToList();
        }

        public void Store(Alert alert)
        {
            using var db = new LiteDatabase(DbPath);
            var AlertDb = db.GetCollection<Alert>("Alerts");
            AlertDb.Upsert(alert);
            //Alerts.Add(alert);

        }
        public void Update(List<Alert> AlertsUpdated)
        {
            using var db = new LiteDatabase(DbPath);
            var AlertDb = db.GetCollection<Alert>("Alerts");
            foreach (var UpAlert in AlertsUpdated)
            {
                AlertDb.Upsert(UpAlert);
            }

        }
    }

}

