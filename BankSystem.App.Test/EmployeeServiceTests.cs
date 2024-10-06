﻿using BankSystem.App.Services;
using BankSystem.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankSystem.App.Tests
{
    public class EmployeeServiceTests
    {
        [Fact]
        public void AddEmployeePositivTest()
        {
            var generatedEmployee = new TestDataGeneratorServise().GenerateListEmployee();
            var employeesService = new EmployeeService(generatedEmployee);

            var newEmployee = new Employee()
            {
                FirstName = "Иванов",
                LastName = "Иван",
                MidlleName = "Иванович",
                Birthday = Convert.ToDateTime("12.12.2000"),
                Email = "wert@dfg.ru",
                PhoneNumber = "00-373-778-5-65-89",
                PassportNumber = "124875",
                PassportSeriya = "3-35",
                Depatment = "Клининговый",
                JobTitle = "Уборщик",
                Salary = 1235
            };

            try
            {
                employeesService.AddEmployee(newEmployee);
            }
            catch (Exception exception)
            {
                Console.WriteLine($"Перехвачено исключение:{exception}");
            }
        }

        [Fact]
        public void AddEmployeeNegativTest()
        {
            var generatedEmployee = new TestDataGeneratorServise().GenerateListEmployee();
            var employeesService = new EmployeeService(generatedEmployee);

            var newEmployee = new Employee()
            {
                FirstName = "",
                LastName = "Иван",
                MidlleName = "",
                Birthday = Convert.ToDateTime("12.12.2000"),
                Email = "wert@dfg.ru",
                PhoneNumber = "00-373-778-5-65-89",
                PassportNumber = "124875",
                PassportSeriya = "3-35",
                Depatment = "Клининговый",
                JobTitle = "Уборщик",
                Salary = 1235
            };

            try
            {
                employeesService.AddEmployee(newEmployee);
            }
            catch (Exception exception)
            {
                Console.WriteLine($"Перехвачено исключение:{exception}");
            }
        }

        [Fact]
        public void UpdateEmployeePositiveTest()
        {
            var generatedEmployee = new TestDataGeneratorServise().GenerateListEmployee();
            var employeesService = new EmployeeService(generatedEmployee);
            var updateEmployee = generatedEmployee[357];

            var updatedEmployee = new Employee()
            {
                PassportNumber = updateEmployee.PassportNumber,
                Birthday = updateEmployee.Birthday,
                Email = updateEmployee.Email,
                PhoneNumber = updateEmployee.PhoneNumber,
                PassportSeriya = updateEmployee.PassportSeriya,
                FirstName = updateEmployee.FirstName,
                LastName = updateEmployee.LastName,
                MidlleName = updateEmployee.MidlleName,
                Depatment = "12344",
                JobTitle = "34564654",
                Salary = 1234545
            };

            try
            {
                employeesService.UpdateWorkEmployee(updatedEmployee);
            }
            catch (Exception exception)
            {
                Console.WriteLine($"Перехвачено исключение:{exception}");
            }
        }

        [Fact]
        public void FilterEmployeeToBirhdayPositiveTest()
        {
            var generatedEmployee = new TestDataGeneratorServise().GenerateListEmployee();

            var countFiltredEmployee = generatedEmployee
               .Where(x => x.Birthday >= Convert.ToDateTime("01.01.2000") 
                           && x.Birthday <= Convert.ToDateTime("31.12.2024"))
               .ToList()
               .Count;

            var employesService = new EmployeeService(generatedEmployee);
            bool equal = false;

            try
            {
                var filtredClients = employesService
                    .FilterEmployee(null,
                        null,
                        null,
                        Convert.ToDateTime("01.01.2000"),
                        Convert.ToDateTime("31.12.2024"));

                if (filtredClients.Count == countFiltredEmployee)
                {
                    equal = true;
                }
            }
            catch (Exception exception)
            {
                Console.WriteLine($"Перехвачено исключение:{exception}");
            }

            Assert.True(equal);
        }

        [Fact]
        public void FilterEmployeeToFIOPositiveTest()
        {
            var generatedEmployee = new TestDataGeneratorServise().GenerateListEmployee();
            var employesService = new EmployeeService(generatedEmployee);
            var foundEmploye = generatedEmployee[145];

            try
            {
                var filtredClients = employesService
                    .FilterEmployee(foundEmploye.FullName(),
                        null,
                        null,
                        null,
                        null);
            }
            catch (Exception exception)
            {
                Console.WriteLine($"Перехвачено исключение:{exception}");
            }
        }

        [Fact]
        public void FilterEmployeeToEmptyPositiveTest()
        {
            var generatedEmployee = new TestDataGeneratorServise().GenerateListEmployee();
            var employesService = new EmployeeService(generatedEmployee);
            var foundEmployee = generatedEmployee[658];

            try
            {
                var filtredClients = employesService
                    .FilterEmployee(null,
                        null,
                        null,
                        null,
                        null);
            }
            catch (Exception exception)
            {
                Console.WriteLine($"Перехвачено исключение:{exception}");
            }
        }

        [Fact]
        public void FilterEmployeeToBirhdayAndPassportNumberPositiveTest()
        {
            var generatedEmployee = new TestDataGeneratorServise().GenerateListEmployee();
            var employesService = new EmployeeService(generatedEmployee);
            var foundEmployee = generatedEmployee[658];

            try
            {
                var filtredClients = employesService
                    .FilterEmployee(null,
                        null,
                        foundEmployee.PassportNumber,
                        Convert.ToDateTime("01.01.2000"),
                        Convert.ToDateTime("31.12.2024"));
            }
            catch (Exception exception)
            {
                Console.WriteLine($"Перехвачено исключение:{exception}");
            }
        }
    }
}
