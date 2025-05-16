using Certifast.Console.Models;
using Certifast.Console.Services.Interface;
using OfficeOpenXml;

namespace Certifast.Console.Services
{
    public class ExcelProcessor : IExcelProcessor
    {
        public List<Certificate> Process(string path)
        {
            //ExcelPackage.LicenseContext = LicenseContext.NonCommercial;
            ExcelPackage.License.SetNonCommercialPersonal("Banana");
            var list = new List<Certificate>();

            using (var pacote = new ExcelPackage(new FileInfo(path)))
            {
                var planilha = pacote.Workbook.Worksheets[0]; // primeira aba
                int totalLinhas = planilha.Dimension.Rows;

                for (int i = 2; i <= totalLinhas; i++) // começa da linha 2 (cabeçalho é linha 1)
                {
                    if (!StatusCertificate(planilha.Cells[i, 9].Text)) continue;

                    string order = planilha.Cells[i, 1].Text;
                    DateTime expiringData = DateTime.Parse(planilha.Cells[i, 2].Text);
                    string name = planilha.Cells[i, 3].Text;
                    string email = planilha.Cells[i, 4].Text;
                    string cell = planilha.Cells[i, 5].Text;
                    string type = planilha.Cells[i, 6].Text;
                    string agent = planilha.Cells[i, 8].Text;
                    string cpf = planilha.Cells[i, 10].Text;
                    string cnpj = planilha.Cells[i, 11].Text;
                    string companyName = planilha.Cells[i, 12].Text;
                    
                    Certificate certificate = new Certificate(order, expiringData, name, email, cell, type, agent, cpf, cnpj, companyName);
                    list.Add(certificate);
                }
            }
            return list;           

        }

        private bool StatusCertificate(string text)
        {
            return text == "Aprovado";

        }

    }
}
