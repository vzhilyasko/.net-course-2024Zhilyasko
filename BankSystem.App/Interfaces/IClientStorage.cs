using BankSystem.Domain.Models;
using BankSystem.Models;

namespace BankSystem.App.Interfaces
{
    public interface IClientStorage : IStorage<Client, Dictionary<Client, List<Account>>>
    {
        public void AddAccount(Client client, Account account);
        public void UpdateAccount(Client client, Account newAccount);
        public void DeleteAccount(Client client, Account account);
    }
}
