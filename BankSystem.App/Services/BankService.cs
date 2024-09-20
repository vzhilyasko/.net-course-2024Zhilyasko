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
            var person = client;
            var employee = new Employee(person, depatment, jobTitle);

            return employee;
        }

    }
}
