using BankSystem.Data.Storages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BankSystem.App.Services;

namespace BankSystem.App.Tests
{
    public class RateUpdaterTests
    {
        [Fact]
        public void UpdateRateAsyncTest()
        {
            BankSystemDbContext _context = new BankSystemDbContext();
            RateUpdaterService rateUpdate = new RateUpdaterService(new ClientStorageEF(_context));

            CancellationTokenSource cancelTokenSource = new CancellationTokenSource();
            CancellationToken token = cancelTokenSource.Token;

            Task task = rateUpdate.UpdateRate(token);
        }
    }
}
