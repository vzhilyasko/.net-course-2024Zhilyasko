using BankSystem.Models;
using BankSystem.Domain.Models;
using BankSystem.App.Exceptions;
using BankSystem.App.Interfaces;

namespace BankSystem.App.Services
{
    public class ClientService
    {
        private readonly IClientStorage _storage; 

        public ClientService(IClientStorage storage)
        {
            _storage = storage;
        }

        public void Add(Client client)
        {
            if (client is null)
            {
                throw new ClientException("Клиент не может быть null");
            }

            if (client.PassportNumber.Length <= 6
                || client.PassportSeriya.Length != 4)
            {
                throw new PassportException("Ошибка в паспортных данных клиента");
            }

            if (client.FirstName == ""
                || client.LastName == ""
                || client.MidlleName == "")
            {
                throw new PersonException("Отсутствует Фамилия, Имя или Отчество клиента");
            }

            if ((DateTime.Now.Year - client.GetAge()) < 18)
            {
                throw new PersonException("Клиенту менне 18 лет");
            }

            _storage.Add(client);
        }

        public void Update(Client client)
        {
            if (client is null)
            {
                throw new ClientException("Клиент не может быть null");
            }

            if (client.PassportNumber.Length <= 6
                || client.PassportSeriya.Length != 4)
            {
                throw new PassportException("Ошибка в паспортных данных клиента");
            }

            if (client.FirstName == ""
                || client.LastName == ""
                || client.MidlleName == "")
            {
                throw new PersonException("Отсутствует Фамилия, Имя или Отчество клиента");
            }

            if ((DateTime.Now.Year - client.GetAge()) < 18)
            {
                throw new PersonException("Клиенту менне 18 лет");
            }
            _storage.Update(client);
        }

        public void Delete(Client client)
        {
            if (client is null)
            {
                throw new ClientException("Клиент не может быть null");
            }

            _storage.Delete(client);
        }

        public void AddAccount(Client client, Account newAccount)
        {
            if (newAccount is null)
            {
                throw new AccountException("Ошибка добавления лицевого счета, счет не может быть null");
            }

            if (newAccount.Currency.Length != 3)
            {
                throw new AccountException("Ошибка добавления лицевого счета, не верно указан код валюты");
            }

            if (client is null)
            {
                throw new ClientException("Клиент не может быть null");
            }
            
            _storage.AddAccount(client, newAccount);
        }

        public void UpdateAccount(Client client, Account updateAccount)
        {
            if (updateAccount is null)
            {
                throw new AccountException("Ошибка добавления лицевого счета, счет не может быть null");
            }

            if (updateAccount.Currency.Length != 3)
            {
                throw new AccountException("Ошибка добавления лицевого счета, не верно указан код валюты");
            }

            if (client is null)
            {
                throw new ClientException("Клиент не может быть null");
            }
            
            _storage.UpdateAccount(client, updateAccount);
        }

        public void DeleteAccount(Client client, Account deleteAccount)
        {
            if (deleteAccount is null)
            {
                throw new AccountException("Ошибка, счет не может быть null");
            }

            if (deleteAccount.Currency.Length != 3)
            {
                throw new AccountException("Ошибка, не верно указан код валюты");
            }

            if (client is null)
            {
                throw new ClientException("Клиент не может быть null");
            }

            _storage.DeleteAccount(client, deleteAccount);
        }
        
        public Dictionary<Client, List<Account>> GetFiltredClient(Func<Client, bool>? filter)
        {
            return _storage.Get(filter);
        }
    }
}
