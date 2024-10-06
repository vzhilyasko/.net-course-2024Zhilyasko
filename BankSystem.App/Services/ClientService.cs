using BankSystem.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BankSystem.Domain.Models;
using BankSystem.App.Exceptions;
using System.Text.Json;

namespace BankSystem.App.Services
{
    public class ClientService
    {
        private readonly Dictionary<Client, List<Account>> Clients; 

        public ClientService(Dictionary <Client, List<Account>> clients)
        {
            Clients = clients;
        }

        public void UpdateAccountsClient(Client client, Account updateAccount)
        {
            if (!this.Clients.ContainsKey(client))
            {
                throw new ClientException("Клиент отсутствует в базе, не возможно редактировать лицевой счет");
            }

            var  updateAccountsClient = Clients[client];

            if (updateAccountsClient == null
                || updateAccountsClient.Count == 0)
            {
                throw new AccountException("Отсутствует лицевой счет для редактирования");
            }

            bool updatedAccount = false;

            updateAccountsClient.ForEach(x =>
            {
                if(x.Currency == updateAccount.Currency)
                {
                    x.Amount = updateAccount.Amount;
                    updatedAccount = true;
                }
            });

            if(updatedAccount)
            {
                Clients[client] = updateAccountsClient;
            }
            else
            {
                throw new AccountException("Отсутствует лицевой счет в необходимой валюте для редактирования");
            }
        }

        public void AddClient(Client client)
        {
            if (this.Clients.ContainsKey(client))
            {
                throw new ClientException("Клиент есть в базе, не возможно добавить");
            }
            
            if (client.FirstName.Length < 1
                || client.LastName.Length < 1
                || client.MidlleName.Length < 1)
            {
                throw new PersonException("У клиента отсутствует Фамилия, Имя и (или) Отчество");
            }

            if (client.GetAge() < 18)
            {
                throw new PersonException("Лицам до 18 лет регистрация запрещена");
            }

            if (client.PassportNumber.Length != 6 )
            {
                throw new PassportException("Отсутствует номер паспорта или длина менее 6 символов");
            }

            if (client.PassportSeriya.Length != 4)
            {
                throw new PassportException("Отсутствует серия паспорта или длина менее 4 символов");
            }

            this.Clients.Add(client, new List<Account>()
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
            var presenceOfClient = this.Clients.ContainsKey(client);

            if (presenceOfClient)
            {
                if (newAccount == null)
                {
                    throw new AccountException("Ошибка добавления лицевого счета, счет не может быть null");
                }

                if (newAccount.Currency.Length != 3)
                {
                    throw new AccountException("Ошибка добавления лицевого счета, не верно указан код валюты");
                }
                
                var accounstClient = this.Clients[client];

                accounstClient.Add(newAccount);

                this.Clients[client] = accounstClient;
            }
            else
            {
                throw new ClientException("Клиент отсутствует в базе");
            }
        }

        public List<Client> FilterСlient(string FIO, string phoneNumber, string passportNumber, DateTime? beginDateTime, DateTime? endDateTime)
        {
            var filtredClient = Clients.Keys.ToList();

            if (FIO != null)
            {
                filtredClient = filtredClient
                    .Where(x => x.FullName() == FIO)
                    .ToList();
            }
            if (phoneNumber != null)
            {
                filtredClient = filtredClient
                    .Where(x => x.PhoneNumber == phoneNumber)
                    .ToList();
            }

            if (passportNumber != null)
            {
                filtredClient = filtredClient
                    .Where(x => x.PassportNumber == passportNumber)
                    .ToList();
            }

            if (beginDateTime != null && endDateTime != null)
            {
                filtredClient = filtredClient
                    .Where(x => x.Birthday >=  beginDateTime && x.Birthday <= endDateTime)
                    .ToList();
            }
            
            return filtredClient;
        }
    }
}
