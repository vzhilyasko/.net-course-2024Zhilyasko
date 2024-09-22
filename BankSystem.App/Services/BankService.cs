using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BankSystem.Models;

namespace BankSystem.App.Services
{
    public class BankService
    {
        public int CalculationSalariesBankOwners(int profitBank, int costsBank, int countOwners)
        {
            int salary = 0;
            salary = (int)(profitBank - costsBank) / countOwners;

            return salary;
        }

        public Employee ConvertClientToEmployee(Client client, string depatment, string jobTitle)
        {
            Person person = client;

            var employee = new Employee()
            {
                FirstName = person.FirstName,
                LastName = person.LastName,
                MidlleName = person.MidlleName,
                Birthday = person.Birthday,
                Email = person.Email,
                PhoneNumber = person.PhoneNumber,
                Depatment = depatment,
                JobTitle = jobTitle,
            };
            return employee;
        }
    }
}
