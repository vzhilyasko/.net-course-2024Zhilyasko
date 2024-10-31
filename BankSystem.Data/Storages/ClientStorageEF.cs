using BankSystem.App.Interfaces;
using BankSystem.Domain.Models;
using BankSystem.Models;
using System.Linq.Expressions;
using System;
using Microsoft.EntityFrameworkCore;

namespace BankSystem.Data.Storages
{
    public class ClientStorageEF : IClientStorage
    {
        private BankSystemDbContext _entitiContext = new BankSystemDbContext();

        public ClientStorageEF(BankSystemDbContext entitiDbContext)
        {
            _entitiContext = entitiDbContext;
        }

        public async Task AddAsync(Client client)
        {
           _entitiContext
                .Clients
                .Add(client);
            
            _entitiContext
                .Accounts
                .Add(new Account()
                {
                    Currency = "USD",
                    Amount = 0,
                    ClientId = client.Id
                });

           await _entitiContext.SaveChangesAsync();
        }

        public async Task UpdateAsync(Client client)
        {
            _entitiContext
                .Clients
                .Update(client);

           await _entitiContext.SaveChangesAsync();
        }

        public async Task DeleteAsync(Client client)
        {
            _entitiContext
                .Clients
                .Remove(client);

           await _entitiContext.SaveChangesAsync();
        }

        public async Task<Client> GetClientByIdAsync(Guid id)
        {
            return await _entitiContext.Clients.FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task AddAccountAsync(Client client, Account account)
        {
            account.ClientId = client.Id;

            _entitiContext
                .Accounts
                .Add(account);

            await _entitiContext.SaveChangesAsync();
        }
        
        public async Task DeleteAccountAsync(Client client, Account account)
        {
            _entitiContext
                .Accounts
                .Remove(account);

            await _entitiContext.SaveChangesAsync();
        }

        public async Task UpdateAccountAsync(Client client, Account account)
        {
           _entitiContext
                .Accounts
                .Update(account);

            await _entitiContext.SaveChangesAsync();
        }

        public async Task<List<Account>> GetClientAccounts(Client client)
        {
            return await _entitiContext
                .Accounts
                .Where(x => x.ClientId == client.Id)
                .ToListAsync();
        }
        
        public async Task<List<Client>> Get(Expression<Func<Client, bool>> filter, int numberPage, int sizePage)
        {
            return await _entitiContext.Clients.Where(filter).Skip((numberPage - 1) * sizePage)
                .Take(sizePage)
                .ToListAsync();
        }

        public async Task<Dictionary<Client, List<Account>>> Get(Func<Client, bool> filter)
        {
            throw new NotImplementedException();
        }

        public async Task<Dictionary<Client, List<Account>>> GetAllClientsAccountsAsync()
        {
            Dictionary<Client, List<Account>> clientsAccouts = new Dictionary<Client, List<Account>>();
            await Task.Run( () =>
            {
                var clients = _entitiContext.Clients.ToList();
                var accounts = _entitiContext.Accounts.ToList();

                clients.ForEach(x =>
                {
                    clientsAccouts.Add(x, accounts.Where(y=>y.ClientId == x.Id).ToList());
                });
            });
            
            return clientsAccouts;
        }

        Dictionary<Client, List<Account>> IStorage<Client, Dictionary<Client, List<Account>>>.Get(Func<Client, bool> filter)
        {
            throw new NotImplementedException();
        }
    }
}
