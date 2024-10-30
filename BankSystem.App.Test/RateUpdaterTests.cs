using BankSystem.Data.Storages;
using BankSystem.App.Services;

namespace BankSystem.App.Tests
{
    public class RateUpdaterTests
    {
        [Fact]
        public async void UpdateRateAsyncTest()
        {
            BankSystemDbContext _context = new BankSystemDbContext();
            RateUpdaterService rateUpdater = new RateUpdaterService(new ClientStorageEF(_context));

            CancellationTokenSource cancelTokenSource = new CancellationTokenSource();
            CancellationToken token = cancelTokenSource.Token;

            Task task = rateUpdater.UpdateRate(token);

            await Task.Delay(500);

            cancelTokenSource.Cancel();
            cancelTokenSource.Dispose();
        }
    }
}
