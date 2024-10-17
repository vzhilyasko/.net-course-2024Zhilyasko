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

        public void Add(Client client)
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

            entitiContext.SaveChanges();
        }

        public void Update(Client client)
        {
            entitiContext
                .Clients
                .Update(client);

            entitiContext.SaveChanges();
        }

        public void Delete(Client client)
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

        public void AddAccount(Client client, Account account)
        {
            account.ClientId = client.Id;

            entitiContext
                .Accounts
                .Add(account);

            entitiContext.SaveChanges();
        }
        
        public void DeleteAccount(Client client, Account account)
        {
            entitiContext
                .Accounts
                .Remove(account);

            entitiContext.SaveChanges();
        }

        public void UpdateAccount(Client client, Account account)
        {
           entitiContext
                .Accounts
                .Add(account);

            entitiContext.SaveChanges();
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
    }
}
