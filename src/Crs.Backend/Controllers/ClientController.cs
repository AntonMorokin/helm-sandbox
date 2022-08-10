using Crs.Backend.Controllers.Requests;
using Crs.Backend.Controllers.Responses;
using Crs.Backend.Logic.Repositories.Interfaces;
using Crs.Backend.Model;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;

namespace Crs.Backend.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public sealed class ClientController : ControllerBase
    {
        private static readonly TimeOnly ZeroTime = TimeOnly.MinValue;

        private readonly IClientsRepository _clientsRepository;

        public ClientController(IClientsRepository clientsRepository)
        {
            _clientsRepository = clientsRepository;
        }

        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(ClientResponse))]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetByIdAsync([FromRoute] int id)
        {
            var client = await _clientsRepository.GetByIdAsync(id);
            if (client is null)
            {
                return NotFound();
            }

            return Ok(CreateResponse(client));
        }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(ClientResponse))]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetByNameAsync([FromQuery] string firstName, [FromQuery] string lastName)
        {
            var client = await _clientsRepository.GetByNameAsync(firstName, lastName);
            if (client is null)
            {
                return NotFound();
            }

            return Ok(CreateResponse(client));
        }

        [HttpPut]
        [ProducesResponseType(StatusCodes.Status201Created, Type = typeof(int))]
        public async Task<IActionResult> CreateNewClientAsync([FromBody] CreateNewClientRequest request)
        {
            var client = new Client(request.FirstName, request.LastName, DateOnly.FromDateTime(request.BirdthDate));
            var id = await _clientsRepository.AddNewClientAsync(client);

            return Created($"client/{id}", id);
        }

        private static ClientResponse CreateResponse(Client client)
            => new()
            {
                Id = client.Id,
                FirstName = client.FirstName,
                LastName = client.LastName,
                BirdthDate = client.BirdthDate.ToDateTime(ZeroTime)
            };
    }
}
