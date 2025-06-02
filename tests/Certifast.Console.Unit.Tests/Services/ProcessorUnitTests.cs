using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using Certifast.Console.Models;
using Certifast.Console.Services;
using Certifast.Console.Services.Interface;
using Moq;
using NUnit.Framework;
using OfficeOpenXml.Drawing;

namespace Certifast.Console.Unit.Tests.Services
{
    [TestFixture]
    class ProcessorUnitTests
    {
        private readonly Mock<IExcelProcessor> _excelProcessor;
        private readonly Mock<IEmailSender> _emailSender;
        private readonly Mock<INoSqlDataBase> _noSqlDataBase;
        private readonly Mock<IProcessor> _iProcessor;


        public ProcessorUnitTests()
        {
            _excelProcessor = new Mock<IExcelProcessor>();
            _emailSender = new Mock<IEmailSender>();
            _noSqlDataBase = new Mock<INoSqlDataBase>();
            _iProcessor = new Mock<IProcessor>();
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
            EmailData email = new EmailData(cert, Body : "This is the body", Subject : "This is the Subject", Expiring : 2 );         
                
            
            bool sent = false;
            datetime datetosend = datetime.today;
            alert alert1 = new alert(order, emailadress, email, sent, datetosend);

            _excelProcessor.Setup(x => x.Process(It.IsAny<string>()))
                .Returns(new List<Certificate> { cert });

            _noSqlDataBase.Setup(x => x.GetAlerts(It.IsAny<DateTime>()))
                .Returns(new List<Alert> { alert });


            // Act
            ProcessFile("arquivo.xlsx", "caminho.xlsx");

            // Assert
            _emailSenderMock.Verify(e => e.Send(alert.EmailAdress, alert.Email), Times.Once);
            Assert.IsTrue(alert.Sent);
        }







        //public void SendDailyAlerts_ShoudGetAlertList_SendEmailAlerts()
        //{
        //    // Arrange

        //    string EmailAdress = "math@mari";
        //    string Order = "144319";
        //    EmailData Email = new EmailData
        //    {
        //        Body = "This is the email body",
        //        Certificate = null,
        //        Expiring = 1,
        //        Subject = "This is de Subject"
        //    };
        //    bool Sent = false;
        //    DateTime DateToSend = DateTime.Today;
        //    Alert alert1 = new Alert(Order, EmailAdress, Email, Sent, DateToSend);
        //    List<Alert> alerts = new List<Alert>();
        //    alerts.Add(alert1);

        //    // Act
        //    _emailSender.Setup(s => s.Send(alert1.EmailAdress, alert1.Email)).Verifiable();



        //}
    }
}
