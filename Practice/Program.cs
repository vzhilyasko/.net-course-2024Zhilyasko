using BankSystem.Models;
using BankSystem.App.Services;
using System.Xml.Linq;
using System.Diagnostics;


Console.ReadKey();

void ListAndDictionary()
{
    var testGeneratorData = new TestDataGeneratorServise();
    var clientsList = testGeneratorData.GenerateListClient(1000);
    string phoneNumberToSearch = clientsList[500].PhoneNumber;

    Stopwatch executionTime = new Stopwatch();
    executionTime.Start();

    var clientFound = clientsList.FirstOrDefault(x => x.PhoneNumber == phoneNumberToSearch);

    executionTime.Stop();

    var clientsDictionary = testGeneratorData.GenerateDictionaryClient();
    phoneNumberToSearch = clientsDictionary.ElementAt(500).Key;

    executionTime.Reset();
    executionTime.Start();

    clientFound = clientsDictionary[phoneNumberToSearch];

    executionTime.Stop();

    var clientsListAgeLess28 = clientsList.Where(x => (x.Birthday.Year + 28) < DateTime.Now.Year).ToList();

    var employeesList = testGeneratorData.GenerateListEmployee(1000);
    var employeesMinSalary = employeesList.Min(x => x.Salary);

    phoneNumberToSearch = clientsDictionary.ElementAt(999).Key;

    executionTime.Reset();
    executionTime.Start();

    clientFound = clientsDictionary[phoneNumberToSearch];

    executionTime.Stop();

    executionTime.Reset();
    executionTime.Start();

    clientFound = clientsDictionary.LastOrDefault(x => x.Key == phoneNumberToSearch).Value;

    executionTime.Stop();
}

void RefAndValType()
{
    var employee = new Employee()
    {
        FirstName = "Иванов",
        LastName = "Иван",
        MidlleName = "Иванович",
        Birthday = Convert.ToDateTime("01.01.2000"),
        Email = "ivanov@gmail.com",
        PhoneNumber = "0779884455",
        Depatment = "Разработка",
        JobTitle = "Программист"
    };

    string UpdateContract(Employee employee)
    {
        return String.Join("|", employee.FirstName, employee.LastName, employee.MidlleName, employee.Birthday,
            employee.PhoneNumber);
    }

    employee.Contract = UpdateContract(employee);

    Console.WriteLine("Контракт: {0}", employee.Contract);
    Console.WriteLine();

    Currency UpdateCurrency(Currency currency, string updateCode, string updateName, double updateExchangeRate)
    {
        currency.Code = updateCode;
        currency.Name = updateName;
        currency.ExchangeRate = updateExchangeRate;

        return currency;
    }

    Currency usdCurrency = new Currency()
    {
        Name = "Доллар США",
        Code = "USD",
        ExchangeRate = 1.235
    };

    usdCurrency = UpdateCurrency(usdCurrency, "USD", "Доллар США", 11.12);

    var bankServise = new BankService();

    var solaru = bankServise.CalculationSalariesBankOwners(1000000, 25, 4);

    var client = new Client
    {
        FirstName = "Сидоров",
        LastName = "Кирилл",
        MidlleName = "Николаевич",
        Birthday = Convert.ToDateTime("01.01.1968"),
        Email = "sid@gmail.com",
        PhoneNumber = "07795459549"
    };

    var convertedEmployee = bankServise.ConvertClientToEmployee(client, "Администрация", "Директор");
}
