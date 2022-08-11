using AutoMapper;
using Crs.Backend.Data;
using Crs.Backend.Logic.Repositories.Interfaces;
using Crs.Backend.Model;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Crs.Backend.Logic.Repositories.Implementations
{
    internal sealed class CarsRepository : ICarsRepository
    {
        private readonly DataContext _dataContext;
        private readonly IMapper _mapper;

        public CarsRepository(DataContext dataContext, IMapper mapper)
        {
            _dataContext = dataContext;
            _mapper = mapper;
        }

        public async Task<IReadOnlyCollection<Car>> GetAsync(int skip, int count)
        {
            var cars = await _dataContext.Cars.AsNoTracking()
                .Skip(skip)
                .Take(count)
                .ToArrayAsync();

            return _mapper.Map<IReadOnlyCollection<Car>>(cars);
        }

        public async Task<Car?> GetByIdAsync(int carId)
        {
            var car = await _dataContext.Cars.AsNoTracking()
                .FirstOrDefaultAsync(x => x.Id == carId);

            return _mapper.Map<Car?>(car);
        }

        public async Task<Car?> GetByNumberAsync(string carNumber)
        {
            var car = await _dataContext.Cars.AsNoTracking()
                .FirstOrDefaultAsync(x => x.Number.ToUpper() == carNumber.ToUpper());

            return _mapper.Map<Car?>(car);
        }

        public async Task<int> CreateNewCarAsync(Car newCar)
        {
            var entry = _dataContext.Cars.Add(new Data.Model.Car
            {
                Number = newCar.Number,
                Brand = newCar.Brand,
                Model = newCar.Model,
                Mileage = newCar.Mileage
            });

            await _dataContext.SaveChangesAsync();

            return entry.Entity.Id;
        }
    }
}
