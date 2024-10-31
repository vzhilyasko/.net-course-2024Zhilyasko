using BankSystem.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankSystem.Domain.Models
{
    public class Account
    {
        public Guid Id { get; set; } = Guid.NewGuid();
        public string Currency { get; set; }
        public int Amount { get; set; }
        public Guid ClientId { get; set; }
        public Client Client { get; set; }
        public DateTime DateCreate { get; set; } = DateTime.Now;
        public DateTime DateUpdated { get; set; } = DateTime.Now;


        private readonly object _locker = new object();

        public void AccountReplenishment(int amount)
        {
            lock (_locker)
            {
                Amount += amount;
            }
        }

        public bool AccountWithdraw(int amount)
        {
            if (amount <= 0)
                throw new ArgumentException("Сумма должна быть более нуля.");

            lock (_locker)
            {
                if (Amount >= amount)
                {
                    Amount -= amount;
                    return true;
                }
                else
                {
                    return false;
                }
            }
        }
    }
}
