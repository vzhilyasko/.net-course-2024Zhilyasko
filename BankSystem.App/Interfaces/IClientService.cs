using BankSystem.Domain.Models;
using BankSystem.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BankSystem.App.Dto;

namespace BankSystem.App.Interfaces
{
   public interface IClientService : IStorage<ClientDto, Dictionary<ClientDto, List<Account>>>
        {
            public Task AddAccountAsync(ClientDto client, Account account);
            public Task UpdateAccountAsync(ClientDto client, Account newAccount);
            public Task DeleteAccountAsync(ClientDto client, Account account);
            public Task<Dictionary<ClientDto, List<Account>>> GetAllClientsAccountsAsync();
        }
}
