using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using BankSystem.Models;
using Bogus;

namespace BankSystem.App.Services
{
    public class TestDataGeneratorServise
    {
        public List<Client> GenerateListClient()
        {
            var fakeClient = new Faker<Client>("ru")
                .RuleFor(x => x.FirstName, g => g.Name.FirstName())
                .RuleFor(x => x.LastName, g=> g.Name.LastName())
                .RuleFor(x => x.MidlleName, g => g.Name.FirstName(g.Person.Gender))
                .RuleFor(x => x.Birthday, g => g.Person.DateOfBirth)
                .RuleFor(x => x.Email, g => g.Person.Email)
                .RuleFor(x => x.PhoneNumber, g => g.Random.Replace("00-373-(###)-#-##-##"));
            return fakeClient.Generate(1000);
        }
        public List<Employee> GenerateListEmployee()
        {
            var fakeEmployee = new Faker<Employee>("ru")
                .RuleFor(x => x.FirstName, g => g.Name.FirstName())
                .RuleFor(x => x.LastName, g => g.Name.LastName())
                .RuleFor(x => x.MidlleName, (g,u) => g.Name.FirstName(g.Person.Gender))
                .RuleFor(x => x.Birthday, g => g.Person.DateOfBirth)
                .RuleFor(x => x.Email, (g,u) => g.Internet.Email(u.FirstName, u.LastName, "idknet.com"))
                .RuleFor(x => x.PhoneNumber, g => g.Random.Replace("00-373-(###)-#-##-##"))
                .RuleFor(x => x.Depatment, g => g.Name.JobDescriptor())
                .RuleFor(x => x.JobTitle, g => g.Name.JobTitle())
                .RuleFor(x => x.Salary, g=> g.Random.Int(5000, 25000));
            return fakeEmployee.Generate(1000);
        }

        public Dictionary<string, Client> GenerateDictionaryClient()
        {
            var generatedClients = GenerateListClient();

            return generatedClients.ToDictionary(x => x.PhoneNumber);
        }
    }
}
