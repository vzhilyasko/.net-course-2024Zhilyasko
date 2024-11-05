using AutoMapper;
using BankSystem.App.Dto;
using BankSystem.Data.Storages;
using BankSystem.Models;
using Microsoft.AspNetCore.Mvc;
using System.Linq.Expressions;

namespace BankSystem.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmployeeController : ControllerBase
    {
        private readonly EmployeeStorageEF _employeeService;
        private readonly IMapper _mapper;

        public EmployeeController(EmployeeStorageEF clientService, IMapper mapper)
        {
            _employeeService = clientService;
            _mapper = mapper;
        }

        [HttpGet("getbyguid")]
        public async Task<IActionResult> GetEmployee([FromQuery] Guid employeeId)
        {
            var employee = await _employeeService.GetEmployeeById(employeeId);

            if (employee == null)
            {
                return NotFound();
            }

            var employeeDto = _mapper.Map<ClientDto>(employee);

            return Ok(employeeDto);
        }

        [HttpPost("add")]
        public async Task<IActionResult> AddEmployee([FromBody] EmployeeDto employeeDto)
        {
            var employee = _mapper.Map<Employee>(employeeDto);

            await _employeeService.AddAsync(employee);

            return Ok(employee);
        }

        [HttpPut("update")]
        public async Task<IActionResult> UpdateEmployee(Guid employeeId, [FromBody] EmployeeDto employeeDto)
        {
            if (employeeDto == null)
            {
                return BadRequest("Данные для обновления не предоставлены");
            }

            var existingEmployee = await _employeeService.GetEmployeeById(employeeId);

            _mapper.Map(employeeDto, existingEmployee);

            await _employeeService.UpdateAsync(existingEmployee);

            return Ok(existingEmployee);
        }

        [HttpDelete("delete")]
        public async Task<IActionResult> DeleteEmployee([FromQuery] Guid employeeId)
        {
            var employee = await _employeeService.GetEmployeeById(employeeId);

            if (employee == null)
            {
                return NotFound();
            }

            await _employeeService.DeleteAsync(employee);

            return Ok();
        }

        [HttpGet("filter")]
        public async Task<ActionResult<List<EmployeeDto>>> SearchEmployees(
            [FromQuery] string name = null,
            [FromQuery] string surname = null,
            [FromQuery] string passportNumber = null,
            [FromQuery] string passportSeriya = null,
            [FromQuery] DateTimeOffset? birthday = null,
            [FromQuery] string phoneNumber = null)
        {
            Expression<Func<Employee, bool>> filter = c =>
                (string.IsNullOrEmpty(name) || c.FullName().Contains(name)) &&
                (string.IsNullOrEmpty(surname) || c.FullName().Contains(surname)) &&
                (string.IsNullOrEmpty(passportNumber) || c.PassportNumber.Contains(passportNumber)) &&
                (string.IsNullOrEmpty(passportSeriya) || c.PassportNumber.Contains(passportSeriya)) &&
                (!birthday.HasValue || c.Birthday == birthday) &&
                (string.IsNullOrEmpty(phoneNumber) || c.PhoneNumber.Contains(phoneNumber));

            var filteredEmployees = _employeeService.Get(filter,1,10);

            var filteredEmployeesDtos = _mapper.Map<List<EmployeeDto>>(filteredEmployees);

            return Ok(filteredEmployeesDtos);
        }
    }
}
