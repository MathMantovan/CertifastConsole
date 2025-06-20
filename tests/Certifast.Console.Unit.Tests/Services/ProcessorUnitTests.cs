using Certifast.Console.Models;
using Certifast.Console.Services;
using Certifast.Console.Services.Interface;
using Moq;
using NUnit.Framework;
using FluentAssertions;


namespace Certifast.Console.Unit.Tests.Services
{
    [TestFixture]
    class ProcessorUnitTests
    {
        private readonly Mock<IExcelProcessor> _excelProcessor;
        private readonly Mock<IEmailSender> _emailSender;
        private readonly Mock<INoSqlDataBase> _noSqlDataBase;
        private Processor _Processor;



        public ProcessorUnitTests()
        {
            _excelProcessor = new Mock<IExcelProcessor>();
            _emailSender = new Mock<IEmailSender>();
            _noSqlDataBase = new Mock<INoSqlDataBase>();
            _Processor = new Processor(_excelProcessor.Object, _emailSender.Object, _noSqlDataBase.Object);

        }

        [Test]
        public void ProcessFile_ShouldSendDailyAlerts_AndMarkAsSent()
        {
            // Arrange
            var cert = new Certificate(
                            order: "001",
                            expiringData: DateTime.Today.AddDays(7),
                            clientName: "Matheus",
                            clientEmail: "ma@ma",
                            cell: "123",
                            type: "e-CPF A1",
                            responseAgent: "Ale",
                            cpf: "123.456.789-00",
                            cnpj: "",
                            companyName: "");
            EmailData email = new EmailData();
                        email.Certificate = cert;
                        email.Body = "This is the body";
                        email.Subject = "This is the Subject";
                        email.Expiring = 2;


            bool sent = false;
            DateTime datetosend = DateTime.Today;
            Alert alert1 = new Alert(cert.Order, cert.ClientEmail, email, sent, datetosend);

            _excelProcessor.Setup(x => x.Process(It.IsAny<string>()))
                .Returns(new List<Certificate> { cert });

            _noSqlDataBase.Setup(x => x.GetAlerts(It.IsAny<DateTime>()))
                .Returns(new List<Alert> { alert1 });


            // Act
            _Processor.ProcessFile("arquivo.xlsx", "caminho.xlsx");

            // Assert
            _emailSender.Verify(e => e.Send(alert1.EmailAdress, alert1.Email), Times.Once);
            alert1.Sent.Should().BeTrue();
            _excelProcessor.Verify(x => x.Process("caminho.xlsx"), Times.Once);
            _noSqlDataBase.Verify(x => x.Store(It.IsAny<Alert>()), Times.AtLeastOnce);
            _noSqlDataBase.Verify(x => x.GetAlerts(DateTime.Today), Times.Once);

        }
    }
}
