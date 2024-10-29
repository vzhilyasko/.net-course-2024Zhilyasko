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

        public async Task AddAsync(Employee employee)
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

            await _storage.AddAsync(employee);
        }

        public async Task UpdateAsync(Employee employee)
        {
            if (employee is null)
            {
                throw new EmployeeException("Сотрудник не может быть null");
            }
            
            await _storage.UpdateAsync(employee);
        }

        public async Task DeleteAsync(Employee employee)
        {
            if (employee is null)
            {
                throw new ClientException("Клиент не может быть null");
            }

            await _storage.DeleteAsync(employee);
        }
        
        public List<Employee> GetFiltredEmployees(Func<Employee, bool>? filter)
        {
            return _storage.Get(filter);
        }
    }
}
