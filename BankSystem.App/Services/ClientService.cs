using BankSystem.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BankSystem.Domain.Models;
using BankSystem.App.Exceptions;
using System.Text.Json;
using BankSystem.Data.Storages;
using System.Security.Principal;

namespace BankSystem.App.Services
{
    public class ClientService
    {
        private readonly ClientStorage _storage; 

        public ClientService(ClientStorage storage)
        {
            _storage = storage;
        }

        public void UpdateAccountsClient(Client client, Account updateAccount)
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
            
            _storage.UpdateAccountsClient(client, updateAccount);
        }

        public void AddClient(Client client)
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

        public void AddAccountClient(Client client, Account newAccount)
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


            _storage.AddAccountClient(client, newAccount);
        }

        public List<Client> FilterСlient(string fullName, string phoneNumber, string passportNumber, DateTime? beginDateTime, DateTime? endDateTime)
        {

            var filtredClient = _storage.FilterСlient(fullName, phoneNumber, passportNumber,
                beginDateTime, endDateTime);
            
            return filtredClient;
        }
    }
}
