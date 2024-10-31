using BankSystem.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BankSystem.App.Services;

namespace BankSystem.App.Tests
{
    public class WithdrawFromAccountsTests
    {
        private List<Account> _accounts;
        private  TestDataGeneratorServise _dataGenerator;

        public WithdrawFromAccountsTests()
        {
            _dataGenerator = new TestDataGeneratorServise();
            _accounts = _dataGenerator.GenerateListAccount();
        }


        private async Task<bool[]> WithdrawFromAccountsAsync(int amount)
        {
            List<Task<bool>> withdrawal = new List<Task<bool>>();

            foreach (var account in _accounts)
            {
                withdrawal.Add(Task.Run(() => account.AccountWithdraw(amount)));
            }

            return await Task.WhenAll(withdrawal);
        }

        [Fact]
        public async Task WithdrawFromAccountsAsyncTest()
        {
            // Arrange
            int amountToWithdraw = 10;

            // Act
            var results = await WithdrawFromAccountsAsync(amountToWithdraw);

            // Assert
            Assert.Equal(_accounts.Count, results.Length);
        }
    }
}
