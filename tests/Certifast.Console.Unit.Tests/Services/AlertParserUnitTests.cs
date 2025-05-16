using System.Security.Cryptography;
using Certifast.Console.Models;
using Certifast.Console.Services;
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
            string order = "";
            DateTime expiringData = DateTime.Today;
            string clientName = "";
            string clientEmail = "";
            string cell = "";
            string type = "";
            string responseAgent = "";
            string cpf = "";
            string cnpj = "";
            string companyName = "";

            var certificate = new Certificate(order, expiringData, clientName, clientEmail, cell, type, responseAgent, cpf, cnpj, companyName);

            //Act
            var ret = AlertParser.GetAlertsFromCertificate(certificate);

            //Assert
            ret.Count.Should().Be(3);
            var firstAlert = ret[0];
            firstAlert.DateToSend.Should().Be(expiringData);

            var secondAlert = ret[1];

            var thirdAlert = ret[2];
        }

        [Test]
        public void GetAlertsFromCertificate_WhenCertificateIsInvalid_ShouldThrowException()
        {

        }
    }
}
