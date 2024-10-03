using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BankSystem.Models;

namespace BankSystem.Data.Storages
{
    public class EmployeeStorage
    {
        private readonly Dictionary<string, Employee> Employees;

        public EmployeeStorage(Dictionary<string, Employee> employees)
        {
            Employees = employees;
        }

        public void Add(Employee employee)
        {
            Employees.Add(employee.PhoneNumber, employee);
        }

        public Employee GetEmployeeMinAge()
        {
            var employeeDateMinAge = Employees
                .Values
                .Max(x=>x.Birthday);

            var employeeMaxAge = Employees
                .Values
                .FirstOrDefault(x=>x.Birthday == employeeDateMinAge);

            return employeeMaxAge;
        }

        public Employee GetEmployeeMaxAge()
        {
            var employeeDateMaxAge = Employees
                .Values
                .Min(x => x.Birthday);

            var employeeMaxAge = Employees
                .Values
                .FirstOrDefault(x => x.Birthday == employeeDateMaxAge);

            return employeeMaxAge;
        }

        public int GetAverageAge()
        {
            var averageAge = (int)Employees
                .Values
                .Average(x => x.GetAge());

            return averageAge;
        }
    }
}
