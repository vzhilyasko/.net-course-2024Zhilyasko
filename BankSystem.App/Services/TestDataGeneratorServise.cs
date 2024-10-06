using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using BankSystem.Domain.Models;
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
                .RuleFor(x => x.Birthday, g => g.Person.DateOfBirth.Date)
                .RuleFor(x => x.Email, g => g.Person.Email)
                .RuleFor(x => x.PhoneNumber, g => g.Random.Replace("00-373-(###)-#-##-##"))
                .RuleFor(x=> x.PassportNumber, g => g.Random.Replace("#######"))
                .RuleFor(x => x.PassportSeriya, g => g.Random.Replace("#-##"));
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
                .RuleFor(x => x.Salary, g=> g.Random.Int(5000, 25000))
                .RuleFor(x => x.PassportNumber, g => g.Random.Replace("#######"))
                .RuleFor(x => x.PassportSeriya, g => g.Random.Replace("#-##"));
            return fakeEmployee.Generate(1000);
        }

        public List<Account> GenerateListAccount()
        {
            var fakeAccount = new Faker<Account>("ru")
                .RuleFor(x => x.Currency, g => g.Finance.Currency().Code);

            return fakeAccount.Generate(10);
        }

        public Dictionary<string, Client> GenerateDictionaryClient()
        {
            var generatedClients = GenerateListClient();

            return generatedClients.ToDictionary(x => x.PhoneNumber);
        }
        public Dictionary<Client, List<Account>> GenerateDictionaryClientAccount()
        {
            var generatedClients = GenerateListClient();
            var generatedAccount = GenerateListAccount();

            Dictionary<Client, List<Account>> generatedDictionary = new Dictionary<Client, List<Account>>();
            Random randomCountAccount = new Random();
            Random randomAmount = new Random();

            generatedClients.ForEach(x =>
            {
                List<Account> accounts = new List<Account>();

                var countAccount = randomCountAccount.Next(1, 5);

                for (int i = 0; i < countAccount; i++)
                {
                    var account = generatedAccount[randomCountAccount.Next(0, 9)];
                    account.Amount = randomAmount.Next(5000, 100000);

                    accounts.Add(account);
                }
                
                generatedDictionary.Add(x, accounts);
            });

            return generatedDictionary;
        }

        public Dictionary<string, Employee> GenerateDictionaryEmployee()
        {
            var generatedEmployees = GenerateListEmployee();

            return generatedEmployees.ToDictionary(x => x.PhoneNumber);
        }
    }
}
