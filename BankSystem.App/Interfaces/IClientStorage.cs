using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BankSystem.Domain.Models;
using BankSystem.Models;

namespace BankSystem.App.Interfaces
{
    public interface IClientStorage : IStorage <Client>
    {
        Dictionary<Client, List<Account>> Get(Func<Client, bool> filter);

        public void AddAccount (Client client, Account account);
       public void UpdateAccount(Client client, Account newAccount);
       public void DeleteAccount(Client client, Account account);
       List<Client> FilterСlient(string fullName, string phoneNumber, string passportNumber, DateTime? beginDateTime, DateTime? endDateTime);
    }
}
