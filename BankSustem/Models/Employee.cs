using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankSystem.Models
{
    public class Employee:Person
    {
        public string Contract { get; set; }
        public string Depatment { get; set; }
        public string JobTitle { get; set; }
        public int Salary { get; set; }

        public override bool Equals(object obj)
        {
            if (obj == null)
                return false;

            if (!(obj is Employee))
                return false;

            var employee = (Employee)obj;

            return employee.FirstName == FirstName
                   && employee.LastName == LastName
                   && employee.MidlleName == MidlleName
                   && employee.Birthday == Birthday
                   && employee.PhoneNumber == PhoneNumber
                   && employee.Email == Email
                   && employee.Depatment == Depatment
                   && employee.JobTitle == JobTitle;
        }

        public static bool operator ==(Employee first, Employee second)
        {
            var equals = first.Equals(second);
            return equals;
        }

        public static bool operator !=(Employee first, Employee second)
        {
            var equals = !first.Equals(second);
            return equals;
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(FirstName + LastName + MidlleName + Email + Birthday + PhoneNumber + Depatment + JobTitle);
        }
    }
}
