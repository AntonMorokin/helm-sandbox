using Crs.Backend.Host.Controllers.Factories;
using Crs.Backend.Host.Controllers.Requests.Create;
using Crs.Backend.Host.Controllers.Requests.Get.Rides;
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
    public sealed class RidesController : ControllerBase
    {
        private readonly IClientsRepository _clientsRepository;
        private readonly ICarsRepository _carsRepository;
        private readonly IRidesRepository _ridesRepository;

        public RidesController(IClientsRepository clientsRepository, ICarsRepository carsRepository, IRidesRepository ridesRepository)
        {
            _ridesRepository = ridesRepository;
            _clientsRepository = clientsRepository;
            _carsRepository = carsRepository;
        }

        [HttpGet("{Id}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(RideResponse))]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetByIdAsync([FromQuery] GetRideByIdRequest request)
        {
            var ride = await _ridesRepository.GetByIdAsync(request.WithClients, request.WithCars, request.Id);
            if (ride is null)
            {
                return NotFound();
            }

            return Ok(ResponseFactory.CreateResponse(ride));
        }

        [HttpGet("all")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IEnumerable<RideResponse>))]
        public async Task<IActionResult> GetAsync([FromQuery] GetRidesRequest request)
        {
            var rides = await _ridesRepository.GetAsync(request.WithClients, request.WithCars, request.Skip ?? 0, request.Count);
            return Ok(rides.Select(ResponseFactory.CreateResponse));
        }

        [HttpGet("client/{ClientId}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IEnumerable<RideResponse>))]
        public async Task<IActionResult> GetForClientAsync([FromQuery] GetRidesForClientRequest request)
        {
            var rides = await _ridesRepository.GetForClientAsync(request.WithCars, request.ClientId, request.Skip ?? 0, request.Count);
            return Ok(rides.Select(ResponseFactory.CreateResponse));
        }

        [HttpGet("car/{CarId}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IEnumerable<RideResponse>))]
        public async Task<IActionResult> GetForCarAsync([FromQuery] GetRidesForCarRequest request)
        {
            var rides = await _ridesRepository.GetForClientAsync(request.WithClients, request.CarId, request.Skip ?? 0, request.Count);
            return Ok(rides.Select(ResponseFactory.CreateResponse));
        }

        [HttpPut]
        [ProducesResponseType(StatusCodes.Status201Created, Type = typeof(int))]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(string))]
        public async Task<IActionResult> CreateNewRideAsync([FromBody] CreateNewRideRequest request)
        {
            var client = await _clientsRepository.GetByIdAsync(request.ClientId);
            if (client is null)
            {
                return NotFound($"Client with id={request.ClientId} does not exist.");
            }

            var car = await _carsRepository.GetByIdAsync(request.CarId);
            if (car is null)
            {
                return NotFound($"Car with id={request.CarId} does not exist.");
            }

            var ride = new Ride(car, client);
            var id = await _ridesRepository.CreateNewRideAsync(ride);

            var location = Url.Action("GetById", new { id })
                 ?? throw new InvalidOperationException("Unable to get location for \"GetByIdAsync\" method");

            return Created(location, id);
        }
    }
}
