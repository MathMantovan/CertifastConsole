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

        string FilePath = "C:\\Users\\avmd_\\OneDrive\\Área de Trabalho\\Renovacoes";

        var watcher = new FileSystemWatcher
        {
            Path = FilePath,
            Filter = "*.xlsx",
            NotifyFilter = NotifyFilters.FileName | NotifyFilters.LastWrite,
            EnableRaisingEvents = true
        };

         watcher.Created += OnArquivoDetectado;
        // Entender pr ta rodando o Task antes de compilar a planilha!!

        //var alertsToday = noSqlDataBase.GetAlerts(DateTime.Today);
        //foreach (var ale in alertsToday)
        //{
        //    EmailSender.Send(ale.EmailAdress, ale.Email);
        //    ale.Sent = true;
        //    noSqlDataBase.Store(ale);
        //}

        var files = Directory.GetFiles(FilePath);
        foreach (var file in files)
        {
            ConsumeFile(Path.GetFileName(file), file);
        }

        System.Console.WriteLine("⏳ Aguardando planilhas para processar...");
        System.Console.ReadLine(); // Mantém o app vivo

    }

    private static void OnArquivoDetectado(object sender, FileSystemEventArgs e)
    {
        ConsumeFile(e.Name, e.FullPath);
        
    }
    public static void ConsumeFile(string fileName, string fullPath)
    {
        try
        {
            System.Console.WriteLine($"\n📥 Arquivo detectado: {fileName}");
            var certificates = ExcelProcessor.Process(fullPath);

            foreach (var certificate in certificates)
            {
                var alerts = AlertParser.GetAlertsFromCertificate(certificate);
                foreach (var ale in alerts)
                {
                    noSqlDataBase.Store(ale);
                }

            }
            //Colocar isso aqui em um metodo:
            var alertsToday = noSqlDataBase.GetAlerts(DateTime.Today);
            foreach (var ale in alertsToday)
            {
                EmailSender.Send(ale.EmailAdress, ale.Email);
                ale.Sent = true;
                //noSqlDataBase.Store(ale);
            }
            //Just to test
            noSqlDataBase.GetAlerts(DateTime.Now);



            File.Delete(fullPath);
            System.Console.WriteLine("✅ Arquivo processado e excluído.");
        }
        catch (Exception ex)
        {
            //Deu erro: ex.Message
            throw;
        }
        
    }


}