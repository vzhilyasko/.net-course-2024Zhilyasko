using BankSystem.App.Services;
using BankSystem.Data.Storages;
using BankSystem.Domain.Models;

namespace BankSystem.Data.Tests
{
    public class ClientStorageFETests
    {
        BankSystemDbContext _context = new BankSystemDbContext();
        
        [Fact]
        public void AddClientToDataBase()
        {
            var clientsStorageEF = new ClientStorageEF(_context);

            var newClient = new TestDataGeneratorServise().GenerateListClient(1)[0];

            clientsStorageEF.AddAsync(newClient);
        }

        [Fact]
        public void ClientUpdateToDataBase()
        {
            var clientsStorageEF = new ClientStorageEF(_context);

            var newClient = new TestDataGeneratorServise().GenerateListClient(1)[0];
            clientsStorageEF.AddAsync(newClient);

            newClient.FirstName = "newName";
            
            clientsStorageEF.UpdateAsync(newClient);
        }

        [Fact]
        public void ClientDeleteToDataBase()
        {
            var clientsStorageEF = new ClientStorageEF(_context);

            var newClient = new TestDataGeneratorServise().GenerateListClient(1)[0];
            clientsStorageEF.AddAsync(newClient);
            
            clientsStorageEF.DeleteAsync(newClient);
        }

        [Fact]
        public void GetClientToIdFromDataBase()
        {
            var clientsStorageEF = new ClientStorageEF(_context);

            var newClient = new TestDataGeneratorServise().GenerateListClient(1)[0];

            clientsStorageEF.AddAsync(newClient);

            var client = clientsStorageEF.GetClientByIdAsync(newClient.Id);

            Assert.NotNull(client);
        }

        [Fact]
        public void AddAccountClientToDataBase()
        {
            var clientsStorageEF = new ClientStorageEF(_context);

            var newClient = new TestDataGeneratorServise().GenerateListClient(1)[0];

            clientsStorageEF.AddAsync(newClient);

            clientsStorageEF.AddAccountAsync(newClient, new Account()
            {
                Amount = 124,
                Currency = "UAN"
            });
        }

        [Fact]
        public async void DeleteAccountClientToDataBase()
        {
            var clientsStorageEF = new ClientStorageEF(_context);

            var newClient = new TestDataGeneratorServise().GenerateListClient(1)[0];

            clientsStorageEF.AddAsync(newClient);

            var accounts = await clientsStorageEF.GetClientAccounts(newClient);
            
            clientsStorageEF.DeleteAccountAsync(newClient, accounts[0]);
        }

        [Fact]
        public void FilterClient()
        {
            var clientsStorageEF = new ClientStorageEF(_context);

            var newClient = new TestDataGeneratorServise().GenerateListClient(1)[0];

            clientsStorageEF.AddAsync(newClient);
            
            var filtredClients = clientsStorageEF
                .Get(c => c.PassportNumber
                    .Contains(newClient.PassportNumber), 1, 2);
        }
    }
}
