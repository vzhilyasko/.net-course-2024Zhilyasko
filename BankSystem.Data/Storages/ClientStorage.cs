using BankSystem.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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
            return _clients.Keys.Where(filter).ToDictionary(c => c, c => _clients[c]);
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

        public List<Client> FilterСlient(string fullName, string phoneNumber, string passportNumber,
            DateTime? beginDateTime, DateTime? endDateTime)
        {
            IEnumerable<Client> filtredClient = _clients.Keys.ToList();
            
            if (fullName != null)
            {
                filtredClient = filtredClient
                    .Where(x => x.FullName().Contains(fullName));
            }
            if (phoneNumber != null)
            {
                filtredClient = filtredClient
                    .Where(x => x.PhoneNumber.StartsWith(phoneNumber));
            }

            if (passportNumber != null)
            {
                filtredClient = filtredClient
                    .Where(x => x.PassportNumber.StartsWith(passportNumber));
            }

            if (beginDateTime != null && endDateTime != null)
            {
                filtredClient = filtredClient
                    .Where(x => x.Birthday >= beginDateTime && x.Birthday <= endDateTime);
            }

            return filtredClient.ToList();
        }

       
    }
}
