using Crs.Backend.Controllers.Requests;
using Crs.Backend.Controllers.Responses;
using Crs.Backend.Logic.Repositories.Interfaces;
using Crs.Backend.Model;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace Crs.Backend.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public sealed class CarController : ControllerBase
    {
        private readonly ICarsRepository _carsRepository;

        public CarController(ICarsRepository carsRepository)
        {
            _carsRepository = carsRepository;
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

            return Ok(CreateResponse(car));
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

            return Ok(CreateResponse(car));
        }

        [HttpPut]
        [ProducesResponseType(StatusCodes.Status201Created, Type = typeof(int))]
        public async Task<IActionResult> CreateNewCarAsync([FromBody] CreateNewCarRequest request)
        {
            var newCar = new Car(request.Number, request.Brand, request.Model, request.Mileage);
            var id = await _carsRepository.AddNewCarAsync(newCar);

            return Created($"car/{id}", id);
        }

        private static CarResponse CreateResponse(Car car)
            => new()
            {
                Id = car.Id,
                Number = car.Number,
                Brand = car.Brand,
                Model = car.Model,
                Mileage = car.Mileage
            };
    }
}
