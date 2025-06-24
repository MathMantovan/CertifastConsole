using Certifast.Console.Services;
using Certifast.Console.Services.Interface;
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

    public static IProcessor processor = new Processor(ExcelProcessor, EmailSender, noSqlDataBase);

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


        var files = Directory.GetFiles(FilePath);
        foreach (var file in files)
        {
            processor.ProcessFile(Path.GetFileName(file), file);
        }

        System.Console.WriteLine("⏳ Aguardando planilhas para processar...");
        System.Console.ReadLine(); // Mantém o app vivo

    }

    private static void OnArquivoDetectado(object sender, FileSystemEventArgs e)
    {

        processor.ProcessFile(e.Name, e.FullPath);

    }
}


