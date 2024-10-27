using BankSystem.Data.Storages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BankSystem.App.Services;

namespace ExportTool.Tests
{
    public class ThreadAndTaskTests
    {
        private TestDataGeneratorServise _dataGenerator = new TestDataGeneratorServise();
        
        [Fact]
        public void AddAmountInAccountsInThreads()
        {
            var account = _dataGenerator.GenerateListAccount()[0];
           
            var threadAdd1 = new Thread(() =>
            {
                for (var i = 0; i < 10; i++)
                {
                    account.Amount += 100;
                }
            });

            var threadAdd2 = new Thread(() =>
            {
                for (var i = 0; i < 10; i++)
                {
                    account.Amount += 100;
                }
            });

            threadAdd1.Start();
            threadAdd2.Start();

            threadAdd1.Join();
            threadAdd2.Join();
        }
    }
}
