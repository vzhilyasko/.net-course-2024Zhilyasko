using BankSystem.Domain.Models;
using BankSystem.Models;

namespace BankSystem.App.Interfaces
{
    public interface IClientStorage : IStorage<Client, Dictionary<Client, List<Account>>>
    {
        public Task AddAccountAsync(Client client, Account account);
        public Task UpdateAccountAsync(Client client, Account newAccount);
        public Task DeleteAccountAsync(Client client, Account account);
        public Task<Dictionary<Client, List<Account>>> GetAllClientsAccountsAsync();
    }
}
