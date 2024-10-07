using BankSystem.App.Exceptions;
using BankSystem.Domain.Models;
using BankSystem.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BankSystem.Data.Storages;

namespace BankSystem.App.Services
{
    public class EmployeeService
    {
        private readonly EmployeeStorage _storage;

        public EmployeeService(EmployeeStorage storage)
        {
            _storage = storage;
        }
        public void UpdateEmployee(Employee employee)
        {
            if (employee is null)
            {
                throw new EmployeeException("Сотрудник не может быть null");
            }
            
            _storage.UpdateEmployee(employee);
        }

        public void AddEmployee(Employee employee)
        {
            if (employee is null)
            {
                throw new EmployeeException("Работник не может быть null");
            }
            
            if (employee.GetAge() < 18)
            {
                throw new PersonException("Лицам до 18 лет регистрация запрещена");
            }

            if (employee.PassportNumber.Length != 6)
            {
                throw new PassportException("Отсутствует номер паспорта или длина менее 6 символов");
            }

            if (employee.PassportSeriya.Length != 4)
            {
                throw new PassportException("Отсутствует серия паспорта или длина менее 4 символов");
            }

            _storage.Add(employee);
        }

        public List<Employee> FilterEmployee(string fullName, string phoneNumber, string passportNumber, DateTime? beginDateTime, DateTime? endDateTime)
        {
            var filtredEmployees = _storage.FiltereEmployees(fullName, phoneNumber, passportNumber,
                beginDateTime, endDateTime);

            return filtredEmployees.ToList();
        }
    }
}
