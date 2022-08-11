using Crs.Backend.Host.Controllers.Factories;
using Crs.Backend.Host.Controllers.Requests.Create;
using Crs.Backend.Host.Controllers.Requests.Get.Clients;
using Crs.Backend.Host.Controllers.Responses;
using Crs.Backend.Logic.Repositories.Interfaces;
using Crs.Backend.Model;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Crs.Backend.Host.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public sealed class ClientsController : ControllerBase
    {
        private readonly IClientsRepository _clientsRepository;

        public ClientsController(IClientsRepository clientsRepository)
        {
            _clientsRepository = clientsRepository;
        }

        [HttpGet("all")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IEnumerable<ClientResponse>))]
        public async Task<IActionResult> GetAsync([FromQuery] GetClientsRequest request)
        {
            var clients = await _clientsRepository.GetAsync(request.Skip ?? 0, request.Count);
            return Ok(clients.Select(ResponseFactory.CreateResponse));
        }

        [HttpGet("{Id}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(ClientResponse))]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetByIdAsync([FromQuery] GetClientByIdRequest request)
        {
            var client = await _clientsRepository.GetByIdAsync(request.Id);
            if (client is null)
            {
                return NotFound();
            }

            return Ok(ResponseFactory.CreateResponse(client));
        }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(ClientResponse))]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetByNameAsync([FromQuery] GetClientByNameRequest request)
        {
            var client = await _clientsRepository.GetByNameAsync(request.FirstName, request.LastName);
            if (client is null)
            {
                return NotFound();
            }

            return Ok(ResponseFactory.CreateResponse(client));
        }

        [HttpPut]
        [ProducesResponseType(StatusCodes.Status201Created, Type = typeof(int))]
        public async Task<IActionResult> CreateNewClientAsync([FromBody] CreateNewClientRequest request)
        {
            var client = new Client(request.FirstName, request.LastName, DateOnly.FromDateTime(request.BirdthDate));
            var id = await _clientsRepository.CreateNewClientAsync(client);

            var location = Url.Action("GetById", new { id })
                ?? throw new InvalidOperationException("Unable to get location for \"GetByIdAsync\" method");

            return Created(location, id);
        }
    }
}
