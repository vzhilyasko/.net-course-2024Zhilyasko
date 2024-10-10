using BankSystem.Models;
using BankSystem.App.Interfaces;
using BankSystem.Domain.Models;

namespace BankSystem.Data.Storages
{
    public class ClientStorage : IClientStorage
    {
        private Dictionary<Client, List<Account>> _clients;

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
        
        public void Add(Client client)
        {
            this._clients.Add(client, new List<Account>()
            {
                new Account()
                {
                    Amount = 0,
                    Currency = "USD"
                }
            });
        }

        public void Update(Client client)
        {
            var updateClient = _clients
                .Keys
                .FirstOrDefault(x => x.PassportNumber == client.PassportNumber
                && x.PassportSeriya == client.PassportSeriya);

            if (updateClient is not null)
            {
                var accounts = _clients[updateClient]
                    .Select(x => new Account(){Amount = x.Amount, Currency = x.Currency})
                    .ToList();

                _clients.Remove(updateClient);
                _clients[client] = accounts;
            }
            else
            {
                throw new KeyNotFoundException("Клиент с данным номером и серией паспорта не найден.");
            }
        }

        public void Delete(Client client)
        {
            _clients.Remove(client);
        }
        
        public void AddAccount(Client client, Account newAccount)
        {
            if (!_clients.ContainsKey(client))
            {
                throw new ArgumentException("Клиент не найден");
            }
            
            var accounstClient = this._clients[client];
            accounstClient.Add(newAccount);

            this._clients[client] = accounstClient;
        }

        public void UpdateAccount(Client client, Account updateAccount)
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

            _clients[client] = updateAccountsClient;
        }

        public void DeleteAccount(Client client, Account account)
        {
            if (_clients.ContainsKey(client))
            {
                var accounts = _clients[client];
                accounts.Remove(account);
            }
        }
    }
}
