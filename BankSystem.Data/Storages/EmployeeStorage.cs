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
        private readonly Dictionary<string, Employee> _employees;

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

        public void UpdateEmployee(Employee employee)
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

        public List<Employee> FiltereEmployees(string fullName, string phoneNumber, string passportNumber,
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




        public Employee GetEmployeeMinAge()
        {
            var employeeDateMinAge = _employees
                .Values
                .Max(x=>x.Birthday);

            var employeeMaxAge = _employees
                .Values
                .FirstOrDefault(x=>x.Birthday == employeeDateMinAge);

            return employeeMaxAge;
        }

        public Employee GetEmployeeMaxAge()
        {
            var employeeDateMaxAge = _employees
                .Values
                .Min(x => x.Birthday);

            var employeeMaxAge = _employees
                .Values
                .FirstOrDefault(x => x.Birthday == employeeDateMaxAge);

            return employeeMaxAge;
        }

        public int GetAverageAge()
        {
            var averageAge = (int)_employees
                .Values
                .Average(x => x.GetAge());

            return averageAge;
        }
    }
}
