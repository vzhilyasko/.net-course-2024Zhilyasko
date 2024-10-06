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
        public string PassportSeriya { get; set; }
        public string PassportNumber { get; set; }

        public int GetAge()
        {
            return DateTime.Now.Year - this.Birthday.Year;
        }

        public string FullName()
        {
            return FirstName + " " + LastName + " " + MidlleName;
        }
    }
}
