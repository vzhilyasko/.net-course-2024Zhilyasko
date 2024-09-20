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

        public Client(Person person)
        {
            FirstName = person.FirstName;
            LastName = person.LastName;
            MidlleName= person.MidlleName;
            Birthday = person.Birthday;
            Email = person.Email;
            PhoneNumber = person.PhoneNumber;
            AccountNumber = SetAccountNumber(person);
        }

        public string SetAccountNumber(Person client)
        {
            return String.Join("|", client.FirstName, client.LastName, client.MidlleName, client.PhoneNumber);
        }


        public string GetInfo()
        {
            return String.Join("|", FirstName, LastName, MidlleName, PhoneNumber);
        }



    }
}
