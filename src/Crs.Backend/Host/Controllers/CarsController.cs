using Crs.Backend.Host.Controllers.Factories;
using Crs.Backend.Host.Controllers.Requests.Create;
using Crs.Backend.Host.Controllers.Requests.Get.Cars;
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
    public sealed class CarsController : ControllerBase
    {
        private readonly ICarsRepository _carsRepository;

        public CarsController(ICarsRepository carsRepository)
        {
            _carsRepository = carsRepository;
        }

        [HttpGet("all")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IEnumerable<CarResponse>))]
        public async Task<IActionResult> GetAsync([FromQuery] GetCarsRequest request)
        {
            var cars = await _carsRepository.GetAsync(request.Skip ?? 0, request.Count);
            return Ok(cars.Select(ResponseFactory.CreateResponse));
        }

        [HttpGet("{Id}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(CarResponse))]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetByIdAsync([FromQuery] GetCarByIdRequest request)
        {
            var car = await _carsRepository.GetByIdAsync(request.Id);
            if (car is null)
            {
                return NotFound();
            }

            return Ok(ResponseFactory.CreateResponse(car));
        }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(CarResponse))]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetByIdAsync([FromQuery] GetCarByNumberRequest request)
        {
            var car = await _carsRepository.GetByNumberAsync(request.Number);
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
