using Certifast.Console.Models;
using Certifast.Console.Services;
using Certifast.Console.Services.Interface;
using FluentAssertions;
using NUnit.Framework;
using System;
using System.IO;
using System.Linq;


namespace Certifast.Console.Unit.Tests.Services
{
    [TestFixture]
    public class NoSqlDataBaseTests
    {
        private const string DbPath = "Test_Certifast.db";
        private NoSqlDataBase _database;

        [SetUp]
        public void Setup()
        {
            if (File.Exists(DbPath))
                File.Delete(DbPath); // Começa com um banco limpo a cada teste
            
            _database = new NoSqlDataBase();
        }

        [Test]
        public void Store_Get_Update_Should_Work_Correctly()
        {
            // Arrange
            var cert = new Certificate(
                order: "001",
                expiringData: DateTime.Today,
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

            var alert = new Alert(cert.Order, cert.ClientEmail, email, sent: false, dateToSend: DateTime.Today);

            // Act - Store
            _database.Store(alert);

            // Act - Get
            var results = _database.GetAlerts(DateTime.Today);
            var resultAlert = results.FirstOrDefault();

            // Assert - Verifica se foi salvo corretamente
            resultAlert.Should().NotBeNull();
            resultAlert.Order.Should().Be(alert.Order);
            resultAlert.Sent.Should().BeFalse();

            // Act - Update
            resultAlert.Sent = true;
            _database.Update(new System.Collections.Generic.List<Alert> { resultAlert });

            // Recarrega para validar a atualização
            var updated = _database.GetAlerts(DateTime.Today).FirstOrDefault();
            updated.Sent.Should().BeTrue(); // Verifica se foi atualizado
        }
    }
}
