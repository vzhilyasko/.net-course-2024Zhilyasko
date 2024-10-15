using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using BankSystem.App.Interfaces;
using BankSystem.Models;

namespace BankSystem.Data.Storages
{
    public class EmployeeStorageEF : IEmployeeStorage
    {
        private readonly BankSystemDbContext entitiContext = new BankSystemDbContext();

        public EmployeeStorageEF(BankSystemDbContext entitiDbContext)
        {
            entitiContext = entitiDbContext;
        }

        public void Add(Employee employee)
        {
            entitiContext
                .Employees
                .Add(employee);

            entitiContext.SaveChanges();
        }
        
        public void Update(Employee employee)
        {
            entitiContext
                .Employees
                .Update(employee);

            entitiContext.SaveChanges();
        }

        public void Delete(Employee employee)
        {
            entitiContext
                .Employees
                .Remove(employee);

            entitiContext.SaveChanges();
        }

        public Employee GetEmployeeById(Guid id)
        {
            return entitiContext
                .Employees
                .FirstOrDefault(x => x.Id == id);
        }

        public List<Employee> Get(Expression<Func<Employee, bool>> filter, int numberPage, int sizePage)
        {
            return entitiContext.Employees.Where(filter).Skip((numberPage - 1) * sizePage)
                .Take(sizePage)
                .ToList();
        }


        public List<Employee> Get(Func<Employee, bool> filter)
        {
            throw new NotImplementedException();
        }
    }
}
