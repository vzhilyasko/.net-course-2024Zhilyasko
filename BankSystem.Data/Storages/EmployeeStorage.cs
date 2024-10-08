using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BankSystem.App.Interfaces;
using BankSystem.Models;

namespace BankSystem.Data.Storages
{
    public class EmployeeStorage : IEmployeeStorage
    {
        private  Dictionary<string, Employee> _employees;

        public EmployeeStorage(Dictionary<string, Employee> employees)
        {
            _employees = employees;
        }

        public Dictionary<string, Employee> Get(Func<string, int, bool> filter)
        {
            if (filter is null)
                throw new ArgumentNullException(nameof(filter));
            return _employees.Keys.Where(filter).ToDictionary(c => c, c => _employees[c]);
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

        public List<Employee> FilterEmployees(string fullName, string phoneNumber, string passportNumber,
            DateTime? beginDateTime, DateTime? endDateTime)
        {
            IEnumerable<Employee> filtredEmployee = _employees.Values.ToList();

            if (fullName != null)
            {
                filtredEmployee = filtredEmployee
                    .Where(x => x.FullName().Contains(fullName));
            }
            if (phoneNumber != null)
            {
                filtredEmployee = filtredEmployee
                    .Where(x => x.PhoneNumber.StartsWith(phoneNumber));
            }

            if (passportNumber != null)
            {
                filtredEmployee = filtredEmployee
                    .Where(x => x.PassportNumber.StartsWith(passportNumber));
            }

            if (beginDateTime != null && endDateTime != null)
            {
                filtredEmployee = filtredEmployee
                    .Where(x => x.Birthday >= beginDateTime && x.Birthday <= endDateTime);
            }

            return filtredEmployee.ToList();
        }

        
    }
}
