using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Certifast.Console.Models;
using Certifast.Console.Services;
using NUnit.Framework;

namespace Certifast.Console.Unit.Tests.Services
{
    [TestFixture]
    public class EmailFormaterUnitTests
    {
        [Test]
        public void BuildEmail_Person_ShoudeBeNameCpfCertificateTypeDaysToExpire()
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

            // Act
            var email = EmailFormater.BuildEmail(cert);

            // Assert
            Assert.That(email.Body, Does.Contain("Olá Matheus"));
            Assert.That(email.Body, Does.Contain("Cpf 123.456.789-00"));
            Assert.That(email.Body, Does.Contain("e-CPF A1"));
            Assert.That(email.Subject, Does.Contain("Seu certificado digital vai vencer em até 7 dias!"));
        }
        

        [Test]
        public void BuildEmail_Company_ShoudeBeNameCnpjCertificateTypeDaysToExpire()
        {
            var cert = new Certificate(
                order: "002",
                expiringData: DateTime.Today.AddDays(1),
                clientName: "João",
                clientEmail: "joao@empresa.com",
                cell: "999",
                type: "e-CNPJ A3",
                responseAgent: "Paula",
                cpf: "123.456.789-00",
                cnpj: "12.345.678/0001-90",
                companyName: "TechCorp");

            var email = EmailFormater.BuildEmail(cert);

            Assert.That(email.Body, Does.Contain("Olá João"));
            Assert.That(email.Body, Does.Contain("a empresa de Cnpj 12.345.678/0001-90"));
            Assert.That(email.Body, Does.Contain("e-CNPJ A3"));
            Assert.That(email.Subject, Does.Contain("Seu certificado digital vai vencer AMANHÃ!!!"));
        }
    }

}

