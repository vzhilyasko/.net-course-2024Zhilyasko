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

            clientsStorageEF.Add(newClient);
        }

        [Fact]
        public void ClientUpdateToDataBase()
        {
            var clientsStorageEF = new ClientStorageEF(_context);

            var newClient = new TestDataGeneratorServise().GenerateListClient(1)[0];
            clientsStorageEF.Add(newClient);

            newClient.FirstName = "newName";
            
            clientsStorageEF.Update(newClient);
        }

        [Fact]
        public void ClientDeleteToDataBase()
        {
            var clientsStorageEF = new ClientStorageEF(_context);

            var newClient = new TestDataGeneratorServise().GenerateListClient(1)[0];
            clientsStorageEF.Add(newClient);
            
            clientsStorageEF.Delete(newClient);
        }

        [Fact]
        public void GetClientToIdFromDataBase()
        {
            var clientsStorageEF = new ClientStorageEF(_context);

            var newClient = new TestDataGeneratorServise().GenerateListClient(1)[0];

            clientsStorageEF.Add(newClient);

            var client = clientsStorageEF.GetClientById(newClient.Id);

            Assert.NotNull(client);
        }

        [Fact]
        public void AddAccountClientToDataBase()
        {
            var clientsStorageEF = new ClientStorageEF(_context);

            var newClient = new TestDataGeneratorServise().GenerateListClient(1)[0];

            clientsStorageEF.Add(newClient);

            clientsStorageEF.AddAccount(newClient, new Account()
            {
                Amount = 124,
                Currency = "UAN"
            });
        }

        [Fact]
        public void DeleteAccountClientToDataBase()
        {
            var clientsStorageEF = new ClientStorageEF(_context);

            var newClient = new TestDataGeneratorServise().GenerateListClient(1)[0];

            clientsStorageEF.Add(newClient);

            var accounts = clientsStorageEF.GetClientAccounts(newClient);
            
            clientsStorageEF.DeleteAccount(newClient, accounts[0]);
        }

        [Fact]
        public void FilterClient()
        {
            var clientsStorageEF = new ClientStorageEF(_context);

            var newClient = new TestDataGeneratorServise().GenerateListClient(1)[0];

            clientsStorageEF.Add(newClient);
            
            var filtredClients = clientsStorageEF
                .Get(c => c.PassportNumber
                    .Contains(newClient.PassportNumber), 1, 2);
        }
    }
}
