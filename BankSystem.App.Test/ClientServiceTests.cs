using BankSystem.App.Services;
using BankSystem.Models;
using BankSystem.Domain.Models;
using BankSystem.Data.Storages;
using System.Collections.Concurrent;

namespace BankSystem.App.Tests
{
    public class ClientServiceTests
    {
        [Fact]
        public async Task AddClientPositivTest()
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
                Birthday = Convert.ToDateTime("12.12.2000"),
                Email = "wert@dfg.ru",
                PhoneNumber = "00-373-778-5-65-89",
                PassportNumber = "124875",
                PassportSeriya = "3-35"
            };
            try
            {
              await  clientsService.AddAsync(newClient);
            }
            catch (Exception exception)
            {
                Console.WriteLine($"Перехвачено исключение:{exception}");
            }
        }
        
        [Fact]
        public async Task AddClientNegativeTest()
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
                Birthday = Convert.ToDateTime("12.12.2000"),
                Email = "wert@dfg.ru",
                PhoneNumber = "00-373-778-5-65-89",
                PassportNumber = "124875",
                PassportSeriya = "3-35"
            };

            try
            {
              await  clientsService.AddAsync(newClient);
            }
            catch (Exception exception)
            {
                Console.WriteLine($"Перехвачено исключение:{exception}");
            }
        }

        [Fact]
        public async Task UpdateClientPositiveTest()
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
                Birthday = client.Birthday,
                Email = client.Email,
                PhoneNumber = client.PhoneNumber,
                PassportNumber = client.PassportNumber,
                PassportSeriya = client.PassportSeriya
            };

            try
            {
              await  clientsService.UpdateAsync(newClient);
            }
            catch (Exception exception)
            {
                Console.WriteLine($"Перехвачено исключение:{exception}");
            }
        }

        [Fact]
        public async Task DeleteClientPositiveTest()
        {
            var generatedClient = new TestDataGeneratorServise().GenerateDictionaryClientAccount();
            var clientStorage = new ClientStorage(generatedClient);
            var clientsService = new ClientService(clientStorage);

            var client = generatedClient.ElementAt(1).Key;
            
            try
            {
               await clientsService.DeleteAsync(client);
            }
            catch (Exception exception)
            {
                Console.WriteLine($"Перехвачено исключение:{exception}");
            }
        }

        [Fact]
        public async Task AddAccountPositiveTest()
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
                await clientsService.AddAccountAsync(client, newAccaunt);
            }
            catch (Exception exception)
            {
                Console.WriteLine($"Перехвачено исключение:{exception}");
            }
        }

        [Fact]
        public async Task UpdateAccountPositiveTest()
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
                await clientsService.UpdateAccount(client, updateAccount);
            }
            catch (Exception exception)
            {
                Console.WriteLine($"Перехвачено исключение:{exception}");
            }
        }

        [Fact]
        public async Task DeleteAccountsPositiveTest()
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
                await clientsService.DeleteAccountAsync(client, deteteAccount);
            }
            catch (Exception exception)
            {
                Console.WriteLine($"Перехвачено исключение:{exception}");
            }
        }

        [Fact]
        public async Task FilterClientToBirhdayPositiveTest()
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
                    .GetFiltredClient(x=>x.Birthday>= Convert.ToDateTime("01.01.2000")
                        && x.Birthday <= Convert.ToDateTime("31.12.2024"))
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
                var filtredClients = clientsService.GetFiltredClient(x => x.FullName().Contains(foundСlient.FirstName));
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
                var filtredClients = clientsService.GetFiltredClient(x => x.PassportNumber == foundСlient.PassportNumber
                                                             && x.Birthday >= foundСlient.Birthday
                                                             && x.Birthday <= Convert.ToDateTime("31.12.2024"));
            }
            catch (Exception exception)
            {
                Console.WriteLine($"Перехвачено исключение:{exception}");
            }
        }

        [Fact]
        public async Task WithdrawFromAccountPositiveTest()
        {
            using var tokenSource = new CancellationTokenSource(TimeSpan.FromSeconds(10));
            var clients = new ConcurrentDictionary<Client, List<Account>>();

            await Task.Run(async () =>
            {
                while (!tokenSource.Token.IsCancellationRequested)
                {
                    _testDataGenerator.ClientsList(10);
                    var clientsWithAccounts = _testDataGenerator.ClientsDictionary();

                    foreach (var client in clientsWithAccounts)
                    {
                        var clientAccounts = await _clientService.GetAsync(client.Key);
                        if (clientAccounts.Count == 0)
                        {
                            await _clientService.AddAsync(client.Key);

                            foreach (var account in client.Value)
                            {
                                await _clientService.AddAccountToClientAsync(client.Key, account);
                            }

                            clients.TryAdd(client.Key, client.Value);
                        }
                    }
                }
            }, tokenSource.Token);

            var tasks = new List<Task>();
            decimal amountToWithdraw = 100;

            foreach (var client in clients.Keys)
            {
                tasks.Add(Task.Run(() => _clientService.WithdrawFromAccountAsync(client, amountToWithdraw)));
            }

            await Task.WhenAll(tasks);

            foreach (var client in clients)
            {
                var clientAccounts = await _clientService.GetAsync(client.Key);
                var updatedAccount = clientAccounts.Values.FirstOrDefault();
                Assert.Equal(0, updatedAccount.FirstOrDefault().Amount);
            }
        }



    }
}
