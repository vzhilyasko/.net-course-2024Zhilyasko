// See https://aka.ms/new-console-template for more information

using BankSystem.Models;

Console.WriteLine("Практика к теме: Типы значений и ссылочные типы");
Console.WriteLine();

var person = new Person("Иванов", "Иван", "Иванович", Convert.ToDateTime("01.01.2000"), "ivanov@gmail.com", "0779884455");
var employee = new Employee(person, "Разработка", "Программист");
employee.Contract = employee.SetContract(employee);


Console.WriteLine("Контракт: {0}; Фамилия: {1}", employee.Contract, employee.FirstName);
Console.WriteLine();

// Меняем Фамилию и дату рождения

employee.FirstName = "Лаврентьев";
employee.Birthday = Convert.ToDateTime("02.02.2020");
employee.Contract = employee.SetContract(employee);

Console.WriteLine("После измения у сотрудника Фамилии и даты рождения");
Console.WriteLine();
Console.WriteLine("Контракт: {0}; Фамилия: {1}", employee.Contract, employee.FirstName);

Console.ReadKey();
