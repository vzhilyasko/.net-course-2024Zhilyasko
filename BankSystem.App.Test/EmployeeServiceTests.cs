using BankSystem.App.Services;
using BankSystem.Models;
using BankSystem.Data.Storages;

namespace BankSystem.App.Tests
{
    public class EmployeeServiceTests
    {
        [Fact]
        public void AddEmployeePositivTest()
        {
            var generatedEmployee = new TestDataGeneratorServise().GenerateDictionaryEmployee();
            var employeeStorage = new EmployeeStorage(generatedEmployee);
            var employeeService = new EmployeeService(employeeStorage);

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
                employeeService.Add(newEmployee);
            }
            catch (Exception exception)
            {
                Console.WriteLine($"Перехвачено исключение:{exception}");
            }
        }

        [Fact]
        public void AddEmployeeNegativTest()
        {
            var generatedEmployee = new TestDataGeneratorServise().GenerateDictionaryEmployee();
            var employeeStorage = new EmployeeStorage(generatedEmployee);
            var employeeService = new EmployeeService(employeeStorage);

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
                employeeService.Add(newEmployee);
            }
            catch (Exception exception)
            {
                Console.WriteLine($"Перехвачено исключение:{exception}");
            }
        }

        [Fact]
        public void UpdateEmployeePositiveTest()
        {
            var generatedEmployee = new TestDataGeneratorServise().GenerateDictionaryEmployee();
            var employeeStorage = new EmployeeStorage(generatedEmployee);
            var employeeService = new EmployeeService(employeeStorage);

            var updateEmployee = generatedEmployee.ElementAt(154).Value;

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
                employeeService.Update(updatedEmployee);
            }
            catch (Exception exception)
            {
                Console.WriteLine($"Перехвачено исключение:{exception}");
            }
        }

        [Fact]
        public void DeleteEmployeePositiveTest()
        {
            var generatedEmployee = new TestDataGeneratorServise().GenerateDictionaryEmployee();
            var employeeStorage = new EmployeeStorage(generatedEmployee);
            var employeeService = new EmployeeService(employeeStorage);

            var deleteEmployee = generatedEmployee.ElementAt(154).Value;
            
            try
            {
                employeeService.Delete(deleteEmployee);
            }
            catch (Exception exception)
            {
                Console.WriteLine($"Перехвачено исключение:{exception}");
            }
        }

        [Fact]
        public void FilterEmployeeToBirhdayPositiveTest()
        {
            var generatedEmployee = new TestDataGeneratorServise().GenerateDictionaryEmployee();
            var employeeStorage = new EmployeeStorage(generatedEmployee);
            var employeeService = new EmployeeService(employeeStorage);

            var countFiltredEmployee = generatedEmployee
                .Where(x => x.Value.Birthday >= Convert.ToDateTime("01.01.2000") 
                            && x.Value.Birthday <= Convert.ToDateTime("31.12.2024"))
               .ToList()
               .Count;

            bool equal = false;

            try
            {
                var filtredClients = employeeService
                    .GetFiltredEmployees(x => x.Birthday >= Convert.ToDateTime("01.01.2000")
                    && x.Birthday <= Convert.ToDateTime("31.12.2024"));

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
            var generatedEmployee = new TestDataGeneratorServise().GenerateDictionaryEmployee();
            var employeeStorage = new EmployeeStorage(generatedEmployee);
            var employeeService = new EmployeeService(employeeStorage);

            var foundEmploye = generatedEmployee.ElementAt(145).Value;

            try
            {
                var filtredClients = employeeService
                    .GetFiltredEmployees(x => x.FullName().Contains(foundEmploye.FullName()));

            }
            catch (Exception exception)
            {
                Console.WriteLine($"Перехвачено исключение:{exception}");
            }
        }
        
        [Fact]
        public void FilterEmployeeToBirhdayAndPassportNumberPositiveTest()
        {
            var generatedEmployee = new TestDataGeneratorServise().GenerateDictionaryEmployee();
            var employeeStorage = new EmployeeStorage(generatedEmployee);
            var employeeService = new EmployeeService(employeeStorage);

            var foundEmployee = generatedEmployee.ElementAt(658).Value;

            try
            {
                var filtredClients = employeeService
                    .GetFiltredEmployees(x => x.PassportNumber == foundEmployee.PassportNumber
                                              && x.Birthday >= foundEmployee.Birthday
                                              && x.Birthday <= Convert.ToDateTime("31.12.2024"));
            }
            catch (Exception exception)
            {
                Console.WriteLine($"Перехвачено исключение:{exception}");
            }
        }
    }
}
