using Certifast.Console.Models;

namespace Certifast.Console.Services.Interface
{
    public interface IExcelProcessor
    {
        List<Certificate> Process(string path);
    }
    
}