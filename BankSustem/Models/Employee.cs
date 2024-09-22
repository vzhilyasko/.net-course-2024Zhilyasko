using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankSystem.Models
{
    //Работник
    public class Employee:Person
    {
        public Employee(string depatment, string jobTitle):base()
        {
            Depatment = depatment;
            JobTitle = jobTitle;
        }

        public Employee(Person person, string depatment, string jobTitle)
        {
            FirstName = person.FirstName;
            LastName = person.LastName;
            MidlleName = person.MidlleName;
            Birthday = person.Birthday;
            Email = person.Email;
            PhoneNumber = person.PhoneNumber;

            Depatment = depatment;
            JobTitle = jobTitle;
        }
    
        public string Contract { get; set; }
        public string Depatment { get; set; }
        public string JobTitle { get; set; }


        public string SetContract(Employee employee)
        {
            return String.Join("|", employee.FirstName, employee.LastName, employee.MidlleName, employee.Birthday,
                employee.PhoneNumber);
        }


        public string GetInfo()
        {
            return String.Join("|", FirstName, LastName, MidlleName, PhoneNumber, Email, Depatment, JobTitle);
        }
    }
}
