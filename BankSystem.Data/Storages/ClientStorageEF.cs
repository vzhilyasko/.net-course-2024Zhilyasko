using BankSystem.App.Interfaces;
using BankSystem.Domain.Models;
using BankSystem.Models;
using System.Linq.Expressions;

namespace BankSystem.Data.Storages
{
    public class ClientStorageEF : IClientStorage
    {
        private readonly BankSystemDbContext entitiContext = new BankSystemDbContext();

        public ClientStorageEF(BankSystemDbContext entitiDbContext)
        {
            entitiContext = entitiDbContext;
        }

        public async Task AddAsync(Client client)
        {
           entitiContext
                .Clients
                .Add(client);
            
            entitiContext
                .Accounts
                .Add(new Account()
                {
                    Currency = "USD",
                    Amount = 0,
                    ClientId = client.Id
                });

            entitiContext.SaveChangesAsync();
        }

        public async Task UpdateAsync(Client client)
        {
            entitiContext
                .Clients
                .Update(client);

            entitiContext.SaveChangesAsync();
        }

        public async Task DeleteAsync(Client client)
        {
            entitiContext
                .Clients
                .Remove(client);

            entitiContext.SaveChanges();
        }

        public Client GetClientById(Guid id)
        {
            return entitiContext
                .Clients
                .FirstOrDefault(x => x.Id == id);
        }

        public async Task AddAccountAsync(Client client, Account account)
        {
            account.ClientId = client.Id;

            entitiContext
                .Accounts
                .Add(account);

            entitiContext.SaveChangesAsync();
        }
        
        public async Task DeleteAccountAsync(Client client, Account account)
        {
            entitiContext
                .Accounts
                .Remove(account);

            entitiContext.SaveChangesAsync();
        }

        public async Task UpdateAccountAsync(Client client, Account account)
        {
           entitiContext
                .Accounts
                .Add(account);

            entitiContext.SaveChangesAsync();
        }

        public List<Account> GetClientAccounts(Client client)
        {
            return entitiContext
                .Accounts
                .Where(x => x.ClientId == client.Id)
                .ToList();
        }
        
        public List<Client> Get(Expression<Func<Client, bool>> filter, int numberPage, int sizePage)
        {
            return entitiContext.Clients.Where(filter).Skip((numberPage - 1) * sizePage)
                .Take(sizePage)
                .ToList();
        }

        public Dictionary<Client, List<Account>> Get(Func<Client, bool> filter)
        {
            throw new NotImplementedException();
        }

        public async Task<Dictionary<Client, List<Account>>> GetAllClientsAccountsAsync()
        {
            Dictionary<Client, List<Account>> clientsAccouts = new Dictionary<Client, List<Account>>();
            await Task.Run(() =>
            {
                var clients = entitiContext.Clients.ToList();
                var accounts = entitiContext.Accounts.ToList();

                clients.ForEach(x =>
                {
                    clientsAccouts.Add(x, accounts.Where(y=>y.ClientId == x.Id).ToList());
                });
                
            });
            
            return clientsAccouts;
        }
    }
}
