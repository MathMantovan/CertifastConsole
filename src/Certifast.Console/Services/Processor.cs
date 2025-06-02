using Certifast.Console.Models;
using Certifast.Console.Services.Exception;
using Certifast.Console.Services.Interface;

namespace Certifast.Console.Services
{
    public class Processor : IProcessor
    {
        private readonly IExcelProcessor _excelProcessor;
        private readonly IEmailSender _emailSender;
        private readonly INoSqlDataBase _noSqlDataBase;

        public Processor(IExcelProcessor excelProcessor, IEmailSender emailSender, INoSqlDataBase noSqlDatabase)
        {
            _excelProcessor = excelProcessor;
            _emailSender = emailSender;
            _noSqlDataBase = noSqlDatabase;
        }

       public void ProcessFile(string fileName, string fullPath)
        {
            try
            {

                System.Console.WriteLine($"\n📥 Arquivo detectado: {fileName}");
                var certificates = _excelProcessor.Process(fullPath);
                MakeAlertsFromCertificates(certificates);
                
                var alertsToday = _noSqlDataBase.GetAlerts(DateTime.Today);
                SendDailyAlerts(alertsToday);

                File.Delete(fullPath);
                System.Console.WriteLine("✅ Arquivo processado e excluído.");
            }
            catch (CertificateException ex)
            {
                //Deu erro: ex.Message
                throw;
            }
        }
        private void SendDailyAlerts(List<Alert> DailyAlerts)
        {
            foreach (var ale in DailyAlerts)
            {
                _emailSender.Send(ale.EmailAdress, ale.Email);
                ale.Sent = true;
                //noSqlDataBase.Store(ale);
            }
        }
        private void MakeAlertsFromCertificates(List<Certificate> certificates)
        {
            foreach (var certificate in certificates)
            {
                var alerts = AlertParser.GetAlertsFromCertificate(certificate);
                foreach (var ale in alerts)
                {
                    _noSqlDataBase.Store(ale);
                }

            }
        }
    }
}
