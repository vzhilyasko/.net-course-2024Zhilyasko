﻿using BankSystem.App.Services;
using BankSystem.Data.Storages;
using BankSystem.Models;

namespace BankSystem.Data.Tests
{
    public class EmployeeStorageTests
    {
        [Fact]
        public void AddClientToEmployeeStorage()
        {
            var employees = new TestDataGeneratorServise().GenerateDictionaryEmployee();
            var employeesStorage = new EmployeeStorage(employees);

            var newEmployee = new Employee()
            {
                FirstName = "Иванов",
                LastName = "Иван",
                MidlleName = "Иванович",
                Birthday = Convert.ToDateTime("12.03.2002"),
                Email = "Ivanov@II.ru",
                PhoneNumber = "00-373-(666)-6-77-88"
            };

            employeesStorage.Add(newEmployee);
        }

        [Fact]
        public void GetFromEmployeeStorageMinAge()
        {
            var employees = new TestDataGeneratorServise().GenerateDictionaryEmployee();
            var employeesStorage = new EmployeeStorage(employees);

            var employeeMinAge = employeesStorage.GetEmployeeMinAge();
        }

        [Fact]
        public void GetFromEmployeeStorageMaxAge()
        {
            var employees = new TestDataGeneratorServise().GenerateDictionaryEmployee();
            var employeesStorage = new EmployeeStorage(employees);

            var employeesMaxAge = employeesStorage.GetEmployeeMaxAge();
        }

        [Fact] public void GetAverageAgeFromEmployeeStorage()
        {
            var employees = new TestDataGeneratorServise().GenerateDictionaryEmployee();
            var employeesStorage = new EmployeeStorage(employees);

            var employeesAverageAge = employeesStorage.GetAverageAge();
        }
    }
}
