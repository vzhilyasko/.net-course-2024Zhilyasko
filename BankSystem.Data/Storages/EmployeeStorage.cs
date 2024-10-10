using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BankSystem.App.Interfaces;
using BankSystem.Models;

namespace BankSystem.Data.Storages
{
    public class EmployeeStorage:IEmployeeStorage
    {
        private  Dictionary<string, Employee> _employees;

        public EmployeeStorage(Dictionary<string, Employee> employees)
        {
            _employees = employees;
        }

        public void Add(Employee employee)
        {
            if (employee.PhoneNumber == "")
            {
                throw new ArgumentException("Отсутствует номер телефона");
            }
            
            if (_employees.ContainsKey(employee.PhoneNumber))
            {
                throw new ArgumentException("Не возможно добавить работника, он уже есть в базе");
            }

            if (employee.Depatment == ""
                || employee.JobTitle == "")
            {
                throw new ArgumentException("Отсутствует должность и подразделение");
            }

            _employees.Add(employee.PhoneNumber, employee);
        }

        public void Update(Employee employee)
        {
            if (employee.PhoneNumber == "")
            {
                throw new ArgumentException("Отсутствует номер телефона");
            }

            if (_employees.ContainsKey(employee.PhoneNumber))
            {
                throw new ArgumentException("Не возможно добавить работника, он уже есть в базе");
            }

            if (employee.Depatment == ""
                || employee.JobTitle == "")
            {
                throw new ArgumentException("Отсутствует должность и подразделение");
            }

            _employees[employee.PhoneNumber]= employee;
        }
        
        public void Delete(Employee employee)
        {
            _employees.Remove(employee.PhoneNumber);
        }
        
        public List<Employee> Get(Func<Employee, bool> filter)
        {
            if (filter is null)
                throw new ArgumentNullException(nameof(filter));

            return _employees.Values
                .Where(filter)
                .ToList();
        }
    }
}
