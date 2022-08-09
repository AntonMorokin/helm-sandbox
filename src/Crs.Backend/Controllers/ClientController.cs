using Crs.Backend.Controllers.Requests;
using Crs.Backend.Controllers.Responses;
using Crs.Backend.Logic.Repositories.Interfaces;
using Crs.Backend.Model;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;

namespace Crs.Backend.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ClientController : ControllerBase
    {
        private readonly TimeOnly ZeroTime = TimeOnly.MinValue;
        private readonly IClientsRepository _clientsRepository;

        public ClientController(IClientsRepository clientsRepository)
        {
            _clientsRepository = clientsRepository;
        }

        [HttpGet("{id}")]
        public async Task<ClientResponse> GetByIdAsync([FromRoute] int id)
        {
            var client = await _clientsRepository.GetByIdAsync(id);
            return new ClientResponse
            {
                Id = client.Id,
                FirstName = client.FirstName,
                LastName = client.LastName,
                BirdthDate = client.BirdthDate.ToDateTime(ZeroTime)
            };
        }

        [HttpPost]
        public async Task<IActionResult> CreateNewClientAsync([FromBody] ClientRequest newClient)
        {
            var client = new Client(newClient.FirstName, newClient.LastName, DateOnly.FromDateTime(newClient.BirdthDate));
            var id = await _clientsRepository.AddNewClientAsync(client);

            return Created($"client/{id}", id);
        }
    }
}
