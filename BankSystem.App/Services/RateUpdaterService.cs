using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;
using System.Text;
using System.Threading.Tasks;
using BankSystem.App.Interfaces;
using BankSystem.Domain.Models;
using BankSystem.Models;

namespace BankSystem.App.Services
{
    public class RateUpdaterService
    {
        private IClientStorage _clientStorage;

        public RateUpdaterService(IClientStorage clientStorage)
        {
            _clientStorage = clientStorage;
        }

        public async Task UpdateRate(CancellationToken cancellationToken)
        {
            while (!cancellationToken.IsCancellationRequested)
            {
                var clients = await _clientStorage.GetAllClientsAccountsAsync();
                var currentDate = DateTime.Now;
                foreach (var clientAccounts in clients)
                {
                    clientAccounts.Value.ForEach(x =>
                    {
                        if ((currentDate - x.DateUpdated).TotalDays >= 30)
                        {
                            x.Amount += 100;
                            _clientStorage.UpdateAccountAsync(clientAccounts.Key, x);
                        }
                    });
                }

                await Task.Delay(11500, cancellationToken);
            }
        }
    }
}
