using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankSystem.Models
{
    //Клиент
    public class Client:Person
    {

        public string AccountNumber { get; set; }

        public Client() : base()
        {
            AccountNumber = SetAccountNumber();
        }

        public string SetAccountNumber()
        {
            return String.Join("|", FirstName, LastName, MidlleName, PhoneNumber);
        }


        public string GetInfo()
        {
            return String.Join("|", FirstName, LastName, MidlleName, PhoneNumber);
        }



    }
}
