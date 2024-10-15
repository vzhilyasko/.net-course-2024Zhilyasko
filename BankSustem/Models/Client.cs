using BankSystem.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankSystem.Models
{
    public class Client:Person
    {


        public ICollection<Account> Accounts { get; set; }

        public override bool Equals(object obj)
        {
            if (obj == null)
                return false;

            if (!(obj is Client))
                return false;

            var client = (Client)obj;

            return client.FirstName == FirstName 
                   && client.LastName == LastName 
                   && client.MidlleName == MidlleName 
                   && client.Birthday == Birthday
                   && client.PhoneNumber == PhoneNumber
                   && client.Email == Email
                   && client.PassportSeriya == PassportSeriya
                   && client.PassportNumber == PassportNumber;
        }

        public static bool operator ==(Client first, Client second)
        {
            var equals = first.Equals(second);
            return equals;
        }

        public static bool operator !=(Client first, Client second)
        {
            var equals = !first.Equals(second);
            return equals;
        }
        public override int GetHashCode()
        {
            return HashCode.Combine(FirstName + LastName + MidlleName + Email + Birthday + PhoneNumber + PassportNumber + PassportSeriya);
        }
    }
}
