using Crs.Backend.Controllers.Factories;
using Crs.Backend.Controllers.Requests.Create;
using Crs.Backend.Controllers.Responses;
using Crs.Backend.Logic.Repositories.Interfaces;
using Crs.Backend.Model;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Crs.Backend.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public sealed class CarsController : ControllerBase
    {
        private readonly ICarsRepository _carsRepository;

        public CarsController(ICarsRepository carsRepository)
        {
            _carsRepository = carsRepository;
        }

        [HttpGet("all")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IEnumerable<CarResponse>))]
        public async Task<IActionResult> GetAsync([FromQuery] int? skip, [FromQuery] int count)
        {
            var cars = await _carsRepository.GetAsync(skip ?? 0, count);
            return Ok(cars.Select(ResponseFactory.CreateResponse));
        }

        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(CarResponse))]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetByIdAsync([FromRoute] int id)
        {
            var car = await _carsRepository.GetByIdAsync(id);
            if (car is null)
            {
                return NotFound();
            }

            return Ok(ResponseFactory.CreateResponse(car));
        }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(CarResponse))]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetByIdAsync([FromQuery] string number)
        {
            var car = await _carsRepository.GetByNumberAsync(number);
            if (car is null)
            {
                return NotFound();
            }

            return Ok(ResponseFactory.CreateResponse(car));
        }

        [HttpPut]
        [ProducesResponseType(StatusCodes.Status201Created, Type = typeof(int))]
        public async Task<IActionResult> CreateNewCarAsync([FromBody] CreateNewCarRequest request)
        {
            var newCar = new Car(request.Number, request.Brand, request.Model, request.Mileage);
            var id = await _carsRepository.CreateNewCarAsync(newCar);

            var location = Url.Action("GetById", new { id })
                ?? throw new InvalidOperationException("Unable to get location for \"GetByIdAsync\" method");

            return Created(location, id);
        }
    }
}
