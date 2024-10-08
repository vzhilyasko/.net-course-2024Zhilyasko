using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BankSystem.Models;

namespace BankSystem.App.Interfaces
{
    public interface IEmployeeStorage : IStorage<Employee>
    {
        Dictionary<string, Employee> Get(Func<string, int, bool> filter);
        List<Employee> FilterEmployees(string fullName, string phoneNumber, string passportNumber, DateTime? beginDateTime, DateTime? endDateTime);
    }
}
