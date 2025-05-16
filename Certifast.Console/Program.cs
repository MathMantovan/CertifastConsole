using Certifast.Console.Models;
using Certifast.Console.Services;
using Certifast.Console.Services.Interface;
using OfficeOpenXml;
// Features:
// - Process excel spreadsheet with certificates and clients
// - Monitor expiring certificates
// - Send e-mail to clients
// - Store processed data into the database
namespace CertiFast.Console;
internal class Program
{

    public static IExcelProcessor ExcelProcessor = new ExcelProcessor();
    public static IEmailSender EmailSender = new EmailSender();
    public static INoSqlDataBase noSqlDataBase = new NoSqlDataBase();


    public static void Main(string[] args)
    {
        System.Console.WriteLine("Hello, World!");

        string File = "C:\\Users\\avmd_\\OneDrive\\Área de Trabalho\\Renovacoes";

        var watcher = new FileSystemWatcher
        {
            Path = File,
            Filter = "*.xlsx",
            NotifyFilter = NotifyFilters.FileName | NotifyFilters.LastWrite,
            EnableRaisingEvents = true
        };

        watcher.Created += OnArquivoDetectado;

        Task.Run(() =>
        {
            
                var alertsToday = noSqlDataBase.GetAlerts(DateTime.Today);
                foreach (var ale in alertsToday)
                {
                    EmailSender.Send(ale.EmailAdress, ale.Email);
                    ale.Sent = true;
                    noSqlDataBase.Store(ale);
                }
            
        });

        System.Console.WriteLine("⏳ Aguardando planilhas para processar...");
        System.Console.ReadLine(); // Mantém o app vivo

    }

    private static void OnArquivoDetectado(object sender, FileSystemEventArgs e)
    {

        System.Console.WriteLine($"\n📥 Arquivo detectado: {e.Name}");
        var certificates = ExcelProcessor.Process(e.FullPath);

        foreach (var certificate in certificates)
        {
            var alerts = AlertParser.GetAlertsFromCertificate(certificate);
            foreach (var ale in alerts)
            {
                noSqlDataBase.Store(ale);
            }

        }



        File.Delete(e.FullPath);
        System.Console.WriteLine("✅ Arquivo processado e excluído.");
    }


}