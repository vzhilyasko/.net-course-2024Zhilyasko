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
    }
}
