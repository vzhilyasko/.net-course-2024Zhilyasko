using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankSystem.Models
{
    public class Person
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string MidlleName { get; set; }
        public DateTime Birthday { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }

        public Person(string firstName, string lastName, string midlleName, DateTime birthday, string email, string phoneNumber)
        {
            FirstName = firstName;
            LastName = lastName;
            MidlleName = midlleName;
            Birthday = birthday;
            Email = email;
            PhoneNumber = phoneNumber;
        }

        protected Person()
        {
            
        }
    }
}
