using BankSystem.App.Services;
using BankSystem.Models;
using BankSystem.Domain.Models;
using System;

namespace BankSystem.App.Tests
{
    public class ClientServiceTests
    {
        [Fact]
        public void AddClientPositivTest()
        {
            var generatedClient = new TestDataGeneratorServise().GenerateDictionaryClientAccount();
            
            var clientsService = new ClientService(generatedClient);

            var client = generatedClient.ElementAt(1).Key;

            var newClient = new Client()
            {
                FirstName = "Иванов",
                LastName = "Иван",
                MidlleName = "Иванович",
                BankAccount = "",
                Birthday = Convert.ToDateTime("12.12.2000"),
                Email = "wert@dfg.ru",
                PhoneNumber = "00-373-778-5-65-89",
                PassportNumber = "124875",
                PassportSeriya = "3-35"
            };
            try
            {
                clientsService.AddClient(newClient);
            }
            catch (Exception exception)
            {
                Console.WriteLine($"Перехвачено исключение:{exception}");
            }
        }
        
        [Fact]
        public void AddClientNegativeTest()
        {
            var generatedClient = new TestDataGeneratorServise().GenerateDictionaryClientAccount();
            var clientsService = new ClientService(generatedClient);
            var client = generatedClient.ElementAt(1).Key;

            var newClient = new Client()
            {
                FirstName = "Иванов",
                LastName = "Иван",
                MidlleName = "",
                BankAccount = "",
                Birthday = Convert.ToDateTime("12.12.2000"),
                Email = "wert@dfg.ru",
                PhoneNumber = "00-373-778-5-65-89",
                PassportNumber = "124875",
                PassportSeriya = "3-35"
            };

            try
            {
                clientsService.AddClient(newClient);
            }
            catch (Exception exception)
            {
                Console.WriteLine($"Перехвачено исключение:{exception}");
            }
        }

        [Fact]
        public void AddAccountToClientPositiveTest()
        {
            var generatedClient = new TestDataGeneratorServise().GenerateDictionaryClientAccount();
            var clientsService = new ClientService(generatedClient);
            var client = generatedClient.ElementAt(10).Key;

            var newAccaunt = new Account()
            {
                Amount = 1258,
                Currency = "HHH"
            };

            try
            {
                clientsService.AddAccountClient(client, newAccaunt);
            }
            catch (Exception exception)
            {
                Console.WriteLine($"Перехвачено исключение:{exception}");
            }
        }

        [Fact]
        public void UpdateAccountsClientPositiveTest()
        {
            var generatedClient = new TestDataGeneratorServise().GenerateDictionaryClientAccount();
            var clientsService = new ClientService(generatedClient);

            var client = generatedClient.ElementAt(10).Key;
            var clientAccaunts = generatedClient[client];

            var countAccount = clientAccaunts.Count;
            var updateAccount = clientAccaunts[countAccount - 1];

            updateAccount.Amount = 1548214;

            try
            {
                clientsService.UpdateAccountsClient(client, updateAccount);
            }
            catch (Exception exception)
            {
                Console.WriteLine($"Перехвачено исключение:{exception}");
            }
        }

        [Fact]
        public void FilterClientToBirhdayPositiveTest()
        {
            var generatedClient = new TestDataGeneratorServise().GenerateDictionaryClientAccount();

            var countFiltredClient = generatedClient
                .Keys
                .ToList()
                .Where(x=> x.Birthday >= Convert.ToDateTime("01.01.2000") 
                           && x.Birthday <= Convert.ToDateTime("31.12.2024"))
                .ToList()
                .Count;
            
            var clientsService = new ClientService(generatedClient);
            bool equal = false;

            try
            {
                var filtredClients = clientsService
                    .FilterСlient(null,
                        null,
                        null,
                        Convert.ToDateTime("01.01.2000"),
                        Convert.ToDateTime("31.12.2024"));
                if (filtredClients.Count == countFiltredClient)
                {
                    equal = true;
                }
            }
            catch (Exception exception)
            {
                Console.WriteLine($"Перехвачено исключение:{exception}");
            }

            Assert.True(equal);
        }

        [Fact]
        public void FilterClientToFIOPositiveTest()
        {
            var generatedClient = new TestDataGeneratorServise().GenerateDictionaryClientAccount();
            var clientsService = new ClientService(generatedClient);
            var foundСlient = generatedClient.ElementAt(145).Key;

            try
            {
                var filtredClients = clientsService
                    .FilterСlient(foundСlient.FullName(),
                        null,
                        null,
                        null,
                        null);
            }
            catch (Exception exception)
            {
                Console.WriteLine($"Перехвачено исключение:{exception}");
            }
        }
       
        [Fact]
        public void FilterClientToEmptyPositiveTest()
        {
            var generatedClient = new TestDataGeneratorServise().GenerateDictionaryClientAccount();
            var clientsService = new ClientService(generatedClient);
            var foundСlient = generatedClient.ElementAt(145).Key;

            try
            {
                var filtredClients = clientsService
                    .FilterСlient(null,
                        null,
                        null,
                        null,
                        null);
            }
            catch (Exception exception)
            {
                Console.WriteLine($"Перехвачено исключение:{exception}");
            }
        }
        
        [Fact]
        public void FilterClientToBirhdayAndPassportNumberPositiveTest()
        {
            var generatedClient = new TestDataGeneratorServise().GenerateDictionaryClientAccount();
            var clientsService = new ClientService(generatedClient);
            var foundСlient = generatedClient.ElementAt(145).Key;

            try
            {
                var filtredClients = clientsService
                    .FilterСlient(null,
                        null,
                        foundСlient.PassportNumber,
                        Convert.ToDateTime("01.01.2000"),
                        Convert.ToDateTime("31.12.2024"));
            }
            catch (Exception exception)
            {
                Console.WriteLine($"Перехвачено исключение:{exception}");
            }
        }
    }
}
