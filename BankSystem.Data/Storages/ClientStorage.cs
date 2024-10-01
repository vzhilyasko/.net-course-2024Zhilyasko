using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BankSystem.Models;

namespace BankSystem.Data.Storages
{
    public class ClientStorage
    {
        private readonly List<Client> Clients;

        public ClientStorage(List<Client> clients)
        {
            Clients = clients;
        }
        
        public void Add(Client client)
        {
            Clients.Add(client);
        }

        public Client GetClientMinAge()
        {
            var clientDateMinAge = Clients
                .Max(x => x.Birthday);

            var clientMinAge = Clients
                .FirstOrDefault(x => x.Birthday == clientDateMinAge);

            return clientMinAge;
        }

        public Client GetClientMaxAge()
        {
            var clientDateMaxAge = Clients
                .Min(x => x.Birthday);

            var clientMaxAge = Clients
                .FirstOrDefault(x => x.Birthday == clientDateMaxAge);
           
            return clientMaxAge;
        }

        public int GetAverageAge()
        {
            var averageAge = (int)Clients
                .Average(x => x.GetAge());
           
            return averageAge;
        }
    }
}
