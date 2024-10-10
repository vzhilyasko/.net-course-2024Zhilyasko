using BankSystem.App.Exceptions;
using BankSystem.Models;
using BankSystem.App.Interfaces;


namespace BankSystem.App.Services
{
    public class EmployeeService
    {
        private readonly IEmployeeStorage _storage;

        public EmployeeService(IEmployeeStorage storage)
        {
            _storage = storage;
        }

        public void Add(Employee employee)
        {
            if (employee is null)
            {
                throw new EmployeeException("Работник не может быть null");
            }

            if (employee.GetAge() < 18)
            {
                throw new PersonException("Лицам до 18 лет регистрация запрещена");
            }

            if (employee.PassportNumber.Length != 6)
            {
                throw new PassportException("Отсутствует номер паспорта или длина менее 6 символов");
            }

            if (employee.PassportSeriya.Length != 4)
            {
                throw new PassportException("Отсутствует серия паспорта или длина менее 4 символов");
            }

            _storage.Add(employee);
        }

        public void Update(Employee employee)
        {
            if (employee is null)
            {
                throw new EmployeeException("Сотрудник не может быть null");
            }
            
            _storage.Update(employee);
        }

        public void Delete(Employee employee)
        {
            if (employee is null)
            {
                throw new ClientException("Клиент не может быть null");
            }

            _storage.Delete(employee);
        }
        
        public List<Employee> GetFiltredEmployees(Func<Employee, bool>? filter)
        {
            return _storage.Get(filter);
        }
    }
}
