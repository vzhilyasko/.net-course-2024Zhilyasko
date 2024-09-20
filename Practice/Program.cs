using BankSystem.Models;
using BankSystem.App.Services;

Console.WriteLine("Практика к теме: Типы значений и ссылочные типы");
Console.WriteLine();


var employee = new Employee("Разработка", "Программист") { FirstName = "Иванов", LastName = "Иван", MidlleName = "Иванович", Birthday = Convert.ToDateTime("01.01.2000"), Email = "ivanov@gmail.com", PhoneNumber = "0779884455" };
employee.Contract = employee.SetContract(employee);

Console.WriteLine("Контракт: {0}; Информация: {1}", employee.Contract, employee.GetInfo());
Console.WriteLine();

// Меняем Фамилию и дату рождения
employee.FirstName = "Лаврентьев";
employee.Birthday = Convert.ToDateTime("02.02.2020");
employee.Contract = employee.SetContract(employee);

Console.WriteLine("После измения у сотрудника Фамилии и даты рождения");
Console.WriteLine();
Console.WriteLine("Контракт: {0}; Информация: {1}", employee.Contract, employee.GetInfo());

Console.WriteLine();

Console.WriteLine("Валюта");
Console.WriteLine();

Currency usdCurrency = new Currency();
usdCurrency.Name = "Доллар США";
usdCurrency.Code = "USD";
usdCurrency.ExchangeRate = 1.235;

Console.WriteLine(usdCurrency.GetInfoCurrency());
Console.WriteLine();

Currency usdUpdateCurrency = new Currency();

usdUpdateCurrency.Name = "Доллар США";
usdUpdateCurrency.Code = "USD";
usdUpdateCurrency.ExchangeRate = 11.235;

usdCurrency.UpdateCurrency(usdUpdateCurrency);

Console.WriteLine("После изменения");
Console.WriteLine(usdCurrency.GetInfoCurrency());
Console.WriteLine();

Console.WriteLine("Преобразование типов");
Console.WriteLine();

var bankServise = new BankService();
var solaru = bankServise.CalculationSalariesBankOwners(1000000, 25, 4);

Console.WriteLine("Прибыль на одного владельца банка: {0}$", solaru);
Console.WriteLine();

var person1 = new Person();

var client = new Client(){ FirstName = "Сидоров", LastName = "Кирилл", MidlleName = "Николаевич", Birthday = Convert.ToDateTime("01.01.1968"), Email = "sid@gmail.com",  PhoneNumber = "07795459549" };
var employee1 = bankServise.ConvertClientToEmployee(client, "Администрация", "Директор");

Console.WriteLine("Клиент преобразованый в сотрудника: {0}", employee1.GetInfo());

// меняем данные клиента
client.FirstName = "Кукушкин";
employee1.Email = "меняем почту";

Console.WriteLine("Сотрудник после смены фамилии у клиента: {0}", employee1.GetInfo());
Console.WriteLine("Клиент после смены почты у сотрудника: {0}$", client.GetInfo());

Console.ReadKey();
