using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BankSystem.Domain.Models;
using BankSystem.Models;

namespace BankSystem.Data.Storages
{
    public class ClientStorage:IClientStorage<Client>
    {
        private readonly Dictionary<Client, List<Account>> _clients;

        public ClientStorage(Dictionary<Client, List<Account>> clients)
        {
            _clients = clients;
        }

        public void UpdateAccountsClient(Client client, Account updateAccount)
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

        public void AddAccountClient(Client client, Account newAccount)
        {
            if (!_clients.ContainsKey(client))
            {
                throw new ArgumentException("Клиент не найден");
            }
            
            var accounstClient = this._clients[client];
            accounstClient.Add(newAccount);

            this._clients[client] = accounstClient;
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
        




        public Client GetClientMinAge()
        {
            var clientDateMinAge = _clients
                .Keys
                .Max(x => x.Birthday);

            var clientMinAge = _clients
                .Keys
                .FirstOrDefault(x => x.Birthday == clientDateMinAge);

            return clientMinAge;
        }

        public Client GetClientMaxAge()
        {
            var clientDateMaxAge = _clients
                .Keys
                .Min(x => x.Birthday);

            var clientMaxAge = _clients
                .Keys
                .FirstOrDefault(x => x.Birthday == clientDateMaxAge);
           
            return clientMaxAge;
        }

        public int GetAverageAge()
        {
            var averageAge = (int)_clients
                .Keys
                .Average(x => x.GetAge());
           
            return averageAge;
        }
    }

    public interface IClientStorage<T>
    {
    }
}
