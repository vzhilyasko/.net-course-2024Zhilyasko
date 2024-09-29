using BankSystem.App.Services;
using BankSystem.Models;

namespace BankSystem.App.Tests
{
    public class EmployeeTests
    {
        [Fact]
        public void GetHashCodeEqvalenceNecessityPositivTest()
        {
            var generatedEmployee = new TestDataGeneratorServise().GenerateListEmployee();

            var employee = generatedEmployee[0];

            var newEmployee = new Employee()
            {
                FirstName = employee.FirstName,
                LastName = employee.LastName,
                MidlleName = employee.MidlleName,
                Birthday = employee.Birthday,
                Email = employee.Email,
                PhoneNumber = employee.PhoneNumber,
                Depatment = employee.Depatment,
                JobTitle = employee.JobTitle
            };
            
            var presenceKey = generatedEmployee[0].Equals(newEmployee);
            
            Assert.True(presenceKey);
        }

        [Fact]
        public void GetEqualityOperatorNecessityPositivTest()
        {
            var generatedEmployee = new TestDataGeneratorServise().GenerateListEmployee();

            var employee = generatedEmployee[0];

            var newEmployee = new Employee()
            {
                FirstName = employee.FirstName,
                LastName = employee.LastName,
                MidlleName = employee.MidlleName,
                Birthday = employee.Birthday,
                Email = employee.Email,
                PhoneNumber = employee.PhoneNumber,
                Depatment = employee.Depatment,
                JobTitle = employee.JobTitle
            };

            bool result = generatedEmployee[0] == newEmployee;
            
            Assert.True(result);
        }

        [Fact]
        public void GetInequalityOperatorNecessityPositivTest()
        {
            var generatedEmployee = new TestDataGeneratorServise().GenerateListEmployee();

            var employee = generatedEmployee[0];

            var newEmployee = new Employee()
            {
                FirstName = employee.FirstName,
                LastName = employee.LastName,
                MidlleName = employee.MidlleName,
                Birthday = employee.Birthday,
                Email = employee.Email,
                PhoneNumber = employee.PhoneNumber,
                Depatment = employee.Depatment,
                JobTitle = employee.JobTitle
            };

            newEmployee.FirstName = newEmployee.FirstName + "new";
            
            bool result = generatedEmployee[0] != newEmployee;

            Assert.True(result);
        }
    }
}
