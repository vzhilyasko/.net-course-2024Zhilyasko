using BankSystem.App.Exceptions;
using BankSystem.Domain.Models;
using BankSystem.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankSystem.App.Services
{
    public class EmployeeService
    {
        private readonly List<Employee> Employees;

        public EmployeeService(List<Employee> employees)
        {
            Employees = employees;
        }
        public void UpdateWorkEmployee(Employee employee)
        {
            if (!this.Employees.Contains(employee))
            {
                throw new EmployeeException("Сотрудник отсутствует в базе, не возможно редактировать данные");
            }
            
            this.Employees.ForEach(x =>
            {
                if (x.FullName() == employee.FullName()
                    && x.PassportNumber == employee.PassportNumber
                    && x.PassportSeriya == employee.PassportSeriya)
                {
                    x.Depatment = employee.Depatment;
                    x.JobTitle = employee.JobTitle;
                    x.Salary = employee.Salary;
                }
            });
        }

        public void AddEmployee(Employee employee)
        {
            if (this.Employees.Contains(employee))
            {
                throw new EmployeeException("Сотрудник уже есть в базе");
            }

            if (employee.FirstName.Length < 1
                || employee.LastName.Length < 1
                || employee.MidlleName.Length < 1)
            {
                throw new PersonException("У сотрудника отсутствует Фамилияи и (или) Имя и (или) Отчество");
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

            this.Employees.Add(employee);
        }

        public List<Employee> FilterEmployee(string FIO, string phoneNumber, string passportNumber, DateTime? beginDateTime, DateTime? endDateTime)
        {
            var filtredEmployees = Employees.ToList();

            if (FIO != null)
            {
                filtredEmployees = filtredEmployees
                    .Where(x => x.FullName() == FIO)
                    .ToList();
            }
            if (phoneNumber != null)
            {
                filtredEmployees = filtredEmployees
                    .Where(x => x.PhoneNumber == phoneNumber)
                    .ToList();
            }

            if (passportNumber != null)
            {
                filtredEmployees = filtredEmployees
                    .Where(x => x.PassportNumber == passportNumber)
                    .ToList();
            }

            if (beginDateTime != null && endDateTime != null)
            {
                filtredEmployees = filtredEmployees
                    .Where(x => x.Birthday >= beginDateTime && x.Birthday <= endDateTime)
                    .ToList();
            }

            return filtredEmployees;
        }
    }
}
