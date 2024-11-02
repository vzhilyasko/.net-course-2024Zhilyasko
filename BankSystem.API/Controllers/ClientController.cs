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
    public class ClientController : ControllerBase
    {
        private readonly ClientStorageEF _clientService;
        private readonly IMapper _mapper;

        public ClientController(ClientStorageEF clientService, IMapper mapper)
        {
            _clientService = clientService;
            _mapper = mapper;
        }

        [HttpGet("Get by guid")]
        public async Task<IActionResult> GetClient([FromQuery] Guid clientId)
        {
            var client = await _clientService.GetClientByIdAsync(clientId);

            if (client == null)
            {
                return NotFound();
            }

            var clientDto = _mapper.Map<ClientDto>(client);

            return Ok(clientDto);
        }

        [HttpPost ("Add")]
        public async Task<IActionResult> AddClient([FromBody] ClientDto clientDto)
        {
            var client = _mapper.Map<Client>(clientDto);

            await _clientService.AddAsync(client);

            return Ok(client);
        }

        [HttpPut("Update")]
        public async Task<IActionResult> UpdateClient(Guid clientId, [FromBody] ClientDto clientDto)
        {
            if (clientDto == null)
            {
                return BadRequest("Данные не были предоставлены");
            }

            var existingClient = await _clientService.GetClientByIdAsync(clientId);

            _mapper.Map(clientDto, existingClient);

            await _clientService.UpdateAsync(existingClient);

            return Ok(existingClient);
        }

        [HttpDelete("Delete")]
        public async Task<IActionResult> DeleteClient([FromQuery] Guid clientId)
        {
            var client = await _clientService.GetClientByIdAsync(clientId);

            if (client == null)
            {
                return NotFound();
            }

            await _clientService.DeleteAsync(client);

            return Ok();
        }

        [HttpGet("Filter")]
        public async Task<ActionResult<List<ClientDto>>> SearchClients(
            CancellationToken cancellationToken,
            [FromQuery] string name = null,
            [FromQuery] string surname = null,
            [FromQuery] string passportNumber = null,
            [FromQuery] string passportSeriya = null,
            [FromQuery] DateTimeOffset? birthday = null,
            [FromQuery] string phoneNumber = null)
        {
            Expression<Func<Client, bool>> filter = c =>
            (string.IsNullOrEmpty(name) || c.FullName().Contains(name)) &&
            (string.IsNullOrEmpty(surname) || c.FullName().Contains(surname)) &&
            (string.IsNullOrEmpty(passportNumber) || c.PassportNumber.Contains(passportNumber)) &&
            (string.IsNullOrEmpty(passportSeriya) || c.PassportNumber.Contains(passportSeriya)) &&
            (!birthday.HasValue || c.Birthday == birthday) &&
            (string.IsNullOrEmpty(phoneNumber) || c.PhoneNumber.Contains(phoneNumber));

            var filteredClients =  _clientService.Get(filter, 1,10);

            var filteredClientDtos = _mapper.Map<List<ClientDto>>(filteredClients);

            return Ok(filteredClientDtos);
        }
    }
}
