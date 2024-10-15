using BankSystem.App.Services;
using BankSystem.Data.Storages;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankSystem.Data.Tests
{
    public class EmployeeStorageFETests
    {
        BankSystemDbContext _context = new BankSystemDbContext();

        [Fact]
        public void AddEmployeeToDataBase()
        {
            var employeesStorageEF = new EmployeeStorageEF(_context);

            var newEmployee = new TestDataGeneratorServise().GenerateListEmployee(1)[0];

            employeesStorageEF.Add(newEmployee);
        }

        [Fact]
        public void EmployeeUpdateToDataBase()
        {
            var employeesStorageEF = new EmployeeStorageEF(_context);

            var newEmployee = new TestDataGeneratorServise().GenerateListEmployee(1)[0];
            
            employeesStorageEF.Add(newEmployee);

            newEmployee.FirstName = "newName";

            employeesStorageEF.Update(newEmployee);
        }

        [Fact]
        public void EmployeeDeleteToDataBase()
        {
            var employeesStorageEF = new EmployeeStorageEF(_context);

            var newEmployee = new TestDataGeneratorServise().GenerateListEmployee(1)[0];
            employeesStorageEF.Add(newEmployee);

            employeesStorageEF.Delete(newEmployee);
        }

        [Fact]
        public void GetEmployeeToIdFromDataBase()
        {
            var employeesStorageEF = new EmployeeStorageEF(_context);

            var newEmployee = new TestDataGeneratorServise().GenerateListEmployee(1)[0];

            employeesStorageEF.Add(newEmployee);

            var employee = employeesStorageEF.GetEmployeeById(newEmployee.Id);

            Assert.NotNull(employee);
        }

        [Fact]
        public void FilterClient()
        {
            var employeesStorageEF = new EmployeeStorageEF(_context);

            var newEmployee = new TestDataGeneratorServise().GenerateListEmployee(1)[0];

            employeesStorageEF.Add(newEmployee);

            var filtredClients = employeesStorageEF
                .Get(c => c.PassportNumber
                    .Contains(newEmployee.PassportNumber), 1, 2);
        }
    }
}
