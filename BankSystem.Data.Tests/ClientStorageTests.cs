using BankSystem.App.Services;
using BankSystem.Data.Storages;
using BankSystem.Domain.Models;
using BankSystem.Models;

namespace BankSystem.Data.Tests
{
    public class ClientStorageTests
    {
        [Fact]
        public void AddClientToClientStorage()
        {
            var clients = new TestDataGeneratorServise().GenerateDictionaryClientAccount();
            var clientsStorage = new ClientStorage(clients);
            
            var newClient = new Client()
            {
                FirstName = "Иванов",
                LastName = "Иван",
                MidlleName = "Иванович",
                Birthday = Convert.ToDateTime("12.03.2002"),
                Email = "Ivanov@II.ru",
                PhoneNumber = "00-373-(666)-6-77-88",
                PassportNumber = "1266546565",
                PassportSeriya = "1-24"
            };
            
            clientsStorage.Add(newClient);
        }

        [Fact]
        public void GetFromClientStorageMinAge()
        {
            var clients = new TestDataGeneratorServise().GenerateDictionaryClientAccount();
            var clientsStorage = new ClientStorage(clients);

            var clientsMinAge = clientsStorage.GetClientMinAge();
        }

        [Fact]
        public void GetFromClientStorageMaxAge()
        {
            var clients = new TestDataGeneratorServise().GenerateDictionaryClientAccount();
            var clientsStorage = new ClientStorage(clients);

            var clientsMaxAge = clientsStorage.GetClientMaxAge();
        }

        [Fact]
        public void GetAverageAgeFromClientStorage()
        {
            var clients = new TestDataGeneratorServise().GenerateDictionaryClientAccount();
            var clientsStorage = new ClientStorage(clients);

            var clientsAverageAge = clientsStorage.GetAverageAge();
        }
    }
}