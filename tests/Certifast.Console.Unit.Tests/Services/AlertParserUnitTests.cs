using System.Security.Cryptography;
using Certifast.Console.Models;
using Certifast.Console.Services;
using Certifast.Console.Services.Exception;
using FluentAssertions;
using NUnit.Framework;

namespace Certifast.Console.Unit.Tests.Services
{
    [TestFixture]
    public class AlertParserUnitTests
    {
        [Test]
        public void GetAlertsFromCertificate_WhenCertificateIsValid_ShouldReturnThreeAlerts()
        {
            //Arrange
            string order = "1955";
            DateTime expiringData = DateTime.Today.AddMonths(1);
            string clientName = "Matheus";
            string clientEmail = "ma@ma";
            string cell = "123";
            string type = "cpf";
            string responseAgent = "ale";
            string cpf = "502";
            string cnpj = "055";
            string companyName = "Dolly";

            var certificate = new Certificate(order, expiringData, clientName, clientEmail, cell, type, responseAgent, cpf, cnpj, companyName);

            //Act
            var ret = AlertParser.GetAlertsFromCertificate(certificate);

            //Assert
            ret.Count.Should().Be(3);
            var firstAlert = ret[0];
            firstAlert.DateToSend.Should().Be(expiringData.AddDays(-1));
            var secondAlert = ret[1];
           secondAlert.DateToSend.Should().Be(expiringData.AddDays(-7));
            var thirdAlert = ret[2];
            thirdAlert.DateToSend.Should().Be(expiringData.AddDays(-30));
        }

        [Test]
        public void GetAlertsFromCertificate_WhenCertificateIsInvalid_ShouldThrowException()
        {
            //Arrange
            string order = "123";
            DateTime expiringData = DateTime.Today.AddMonths(1);
            string clientName = "Matheus";
            string clientEmail = "";
            string cell = "123";
            string type = "cpf";
            string responseAgent = "ale";
            string cpf = "502";
            string cnpj = "055";
            string companyName = "Dolly";
            var certificate = new Certificate(order, expiringData, clientName, clientEmail, cell, type, responseAgent, cpf, cnpj, companyName);

            //Act
            var ret = Assert.Throws<CertificateException>(() => AlertParser.GetAlertsFromCertificate(certificate));

            //Assert
            Assert.That(ret.Message, Is.EqualTo("Certificado sem Email para contato"));
        }
    }
}
