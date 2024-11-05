using BankSystem.Models;
using BankSystem.Domain.Models;
using BankSystem.App.Exceptions;
using BankSystem.App.Interfaces;
using System.Linq.Expressions;
using AutoMapper;
using BankSystem.App.Dto;

namespace BankSystem.App.Services
{
    public class ClientService : IClientService
    {
        private readonly IClientStorage _storage;
        private readonly IMapper _mapper;
       
        public ClientService(IClientStorage storage)
        {
            _storage = storage;
        }
        
        public async Task AddAsync(ClientDto clientDto)
        {
            var client = _mapper.Map<Client>(clientDto);

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

            await _storage.AddAsync(client);
        }

        public async Task UpdateAsync(ClientDto clientDto)
        {
            var client = _mapper.Map<Client>(clientDto);

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
            await _storage.UpdateAsync(client);
        }

        public async Task DeleteAsync(ClientDto clientDto)
        {
            var client = _mapper.Map<Client>(clientDto);

            if (client is null)
            {
                throw new ClientException("Клиент не может быть null");
            }

            await _storage.DeleteAsync(client);
        }

        public async Task AddAccountAsync(ClientDto clientDto, Account newAccount)
        {
            var client = _mapper.Map<Client>(clientDto);

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
            
            await _storage.AddAccountAsync(client, newAccount);
        }

        public async Task UpdateAccount(ClientDto clientDto, Account updateAccount)
        {
            var client = _mapper.Map<Client>(clientDto);
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
            
            await _storage.UpdateAccountAsync(client, updateAccount);
        }

        public async Task  DeleteAccountAsync(ClientDto clientDto, Account deleteAccount)
        {
            var client = _mapper.Map<Client>(clientDto);
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

            await _storage.DeleteAccountAsync(client, deleteAccount);
        }
        
        public  Dictionary<Client, List<Account>> GetFiltredClient(Func<Client, bool>? filter)
        {
            return _storage.Get(filter);
        }

        public Task UpdateAccountAsync(ClientDto client, Account newAccount)
        {
            throw new NotImplementedException();
        }

        public Task<Dictionary<ClientDto, List<Account>>> GetAllClientsAccountsAsync()
        {
            throw new NotImplementedException();
        }

        public Dictionary<ClientDto, List<Account>> Get(Func<ClientDto, bool> filter)
        {
            throw new NotImplementedException();
        }
    }
}
