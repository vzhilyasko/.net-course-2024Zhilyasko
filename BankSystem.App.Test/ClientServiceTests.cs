using BankSystem.App.Services;
using BankSystem.Models;
using BankSystem.Domain.Models;
using System;
using BankSystem.App.Interfaces;
using BankSystem.Data.Storages;

namespace BankSystem.App.Tests
{
    public class ClientServiceTests
    {
        [Fact]
        public void AddClientPositivTest()
        {
            var generatedClient = new TestDataGeneratorServise().GenerateDictionaryClientAccount();
            var clientStorage = new ClientStorage(generatedClient);
            var clientsService = new ClientService(clientStorage);

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
                clientsService.Add(newClient);
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
            var clientStorage = new ClientStorage(generatedClient);
            var clientsService = new ClientService(clientStorage);

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
                clientsService.Add(newClient);
            }
            catch (Exception exception)
            {
                Console.WriteLine($"Перехвачено исключение:{exception}");
            }
        }

        [Fact]
        public void UpdateClientPositiveTest()
        {
            var generatedClient = new TestDataGeneratorServise().GenerateDictionaryClientAccount();
            var clientStorage = new ClientStorage(generatedClient);
            var clientsService = new ClientService(clientStorage);

            var client = generatedClient.ElementAt(1).Key;

            var newClient = new Client()
            {
                FirstName = client.FirstName,
                LastName = client.LastName,
                MidlleName = "client.MidlleName",
                BankAccount = "",
                Birthday = client.Birthday,
                Email = client.Email,
                PhoneNumber = client.PhoneNumber,
                PassportNumber = client.PassportNumber,
                PassportSeriya = client.PassportSeriya
            };

            try
            {
                clientsService.Update(newClient);
            }
            catch (Exception exception)
            {
                Console.WriteLine($"Перехвачено исключение:{exception}");
            }
        }

        [Fact]
        public void DeleteClientPositiveTest()
        {
            var generatedClient = new TestDataGeneratorServise().GenerateDictionaryClientAccount();
            var clientStorage = new ClientStorage(generatedClient);
            var clientsService = new ClientService(clientStorage);

            var client = generatedClient.ElementAt(1).Key;
            
            try
            {
                clientsService.Delete(client);
            }
            catch (Exception exception)
            {
                Console.WriteLine($"Перехвачено исключение:{exception}");
            }
        }

        [Fact]
        public void AddAccountPositiveTest()
        {
            var generatedClient = new TestDataGeneratorServise().GenerateDictionaryClientAccount();
            var clientStorage = new ClientStorage(generatedClient);
            var clientsService = new ClientService(clientStorage);

            var client = generatedClient.ElementAt(10).Key;

            var newAccaunt = new Account()
            {
                Amount = 1258,
                Currency = "HHH"
            };

            try
            {
                clientsService.AddAccount(client, newAccaunt);
            }
            catch (Exception exception)
            {
                Console.WriteLine($"Перехвачено исключение:{exception}");
            }
        }

        [Fact]
        public void UpdateAccountPositiveTest()
        {
            var generatedClient = new TestDataGeneratorServise().GenerateDictionaryClientAccount();
            var clientStorage = new ClientStorage(generatedClient);
            var clientsService = new ClientService(clientStorage);

            var client = generatedClient.ElementAt(10).Key;
            var clientAccaunts = generatedClient[client];

            var countAccount = clientAccaunts.Count;
            var updateAccount = clientAccaunts[countAccount - 1];

            updateAccount.Amount = 1548214;

            try
            {
                clientsService.UpdateAccount(client, updateAccount);
            }
            catch (Exception exception)
            {
                Console.WriteLine($"Перехвачено исключение:{exception}");
            }
        }

        [Fact]
        public void DeleteAccountsPositiveTest()
        {
            var generatedClient = new TestDataGeneratorServise().GenerateDictionaryClientAccount();
            var clientStorage = new ClientStorage(generatedClient);
            var clientsService = new ClientService(clientStorage);

            var client = generatedClient.ElementAt(10).Key;
            var clientAccaunts = generatedClient[client];

            var countAccount = clientAccaunts.Count;
            var deteteAccount = clientAccaunts[countAccount - 1];

            deteteAccount.Amount = 1548214;

            try
            {
                clientsService.DeleteAccount(client, deteteAccount);
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
            var clientStorage = new ClientStorage(generatedClient);
            var clientsService = new ClientService(clientStorage);

            var countFiltredClient = generatedClient
                .Keys
                .ToList()
                .Where(x=> x.Birthday >= Convert.ToDateTime("01.01.2000") 
                           && x.Birthday <= Convert.ToDateTime("31.12.2024"))
                .ToList()
                .Count;
            
           bool equal = false;

            try
            {
                var filtredClients = clientsService
                    .FilterСlient(null,
                        null,
                        null,
                        Convert.ToDateTime("01.01.2000"),
                        Convert.ToDateTime("31.12.2024"))
                    .ToList();
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
        public void FilterClientToFullNamePositiveTest()
        {
            var generatedClient = new TestDataGeneratorServise().GenerateDictionaryClientAccount();
            var clientStorage = new ClientStorage(generatedClient);
            var clientsService = new ClientService(clientStorage);

            var foundСlient = generatedClient.ElementAt(145).Key;

            try
            {
                var filtredClients = clientsService
                    .FilterСlient(foundСlient.FirstName,
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
            var clientStorage = new ClientStorage(generatedClient);
            var clientsService = new ClientService(clientStorage);

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
            var clientStorage = new ClientStorage(generatedClient);
            var clientsService = new ClientService(clientStorage);

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
