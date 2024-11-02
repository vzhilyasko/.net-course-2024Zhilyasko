using BankSystem.Models;
using BankSystem.App.Interfaces;
using BankSystem.Domain.Models;
using System.Linq.Expressions;

namespace BankSystem.Data.Storages
{
    public class ClientStorage : IClientStorage
    {
        private Dictionary<Client, List<Account>> _clients;
        private IClientStorage _clientStorageImplementation;

        public ClientStorage(Dictionary<Client, List<Account>> clients)
        {
            _clients = clients;
        }

        public Dictionary<Client, List<Account>> Get(Func<Client, bool> filter)
        {
            if (filter is null)
                throw new ArgumentNullException(nameof(filter));

            return _clients
                .Where(kvp => filter(kvp.Key))
                .ToDictionary(kvp => kvp.Key, kvp => kvp.Value);
        }
        
        public async Task AddAsync(Client client)
        {
            await Task.Run(() =>
            {
                this._clients.Add(client, new List<Account>()
                {
                    new Account()
                    {
                        Amount = 0,
                        Currency = "USD"
                    }
                });
            });
        }

        public async Task UpdateAsync(Client client)
        {
            await Task.Run(() =>
            {
                var updateClient = _clients
                    .Keys
                    .FirstOrDefault(x => x.PassportNumber == client.PassportNumber
                                         && x.PassportSeriya == client.PassportSeriya);

                if (updateClient is not null)
                {
                    var accounts = _clients[updateClient]
                        .Select(x => new Account() { Amount = x.Amount, Currency = x.Currency })
                        .ToList();

                    _clients.Remove(updateClient);
                    _clients[client] = accounts;
                }
                else
                {
                    throw new KeyNotFoundException("Клиент с данным номером и серией паспорта не найден.");
                }
            });
        }

        public async Task DeleteAsync(Client client)
        {
            await Task.Run(() => { _clients.Remove(client); });
        }
        
        public async Task AddAccountAsync(Client client, Account newAccount)
        {
            if (!_clients.ContainsKey(client))
            {
                throw new ArgumentException("Клиент не найден");
            }
            
            await Task.Run(() =>
            {
                var accounstClient = this._clients[client];
                accounstClient.Add(newAccount);

                this._clients[client] = accounstClient;
            });
        }

        public async Task UpdateAccountAsync(Client client, Account updateAccount)
        {
            if (!_clients.ContainsKey(client))
            {
                throw new ArgumentException("Клиент не найден");
            }

            var updateAccountsClient = _clients[client];

            bool accountNotUpdate = false;

            updateAccountsClient.ForEach(x =>
            {
                if (x.Currency == updateAccount.Currency)
                {
                    x.Amount = updateAccount.Amount;
                    accountNotUpdate = true;
                }
            });

            if (!accountNotUpdate)
            {
                throw new ArgumentException("Аккаунт не найден");
            }

            await Task.Run(() =>
            {
                _clients[client] = updateAccountsClient;
            });
        }

        public async Task DeleteAccountAsync(Client client, Account account)
        {
            if (_clients.ContainsKey(client))
            {
                await Task.Run(() =>
                {
                    var accounts = _clients[client];
                    accounts.Remove(account);
                });
            }
        }

        public Task<Dictionary<Client, List<Account>>> GetAllClientsAccountsAsync()
        {
            throw new NotImplementedException();
        }
    }
}
