using AutoMapper;
using Crs.Backend.Data;
using Crs.Backend.Logic.Repositories.Interfaces;
using Crs.Backend.Model;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Crs.Backend.Logic.Repositories.Implementations
{
    internal sealed class RidesRepository : IRidesRepository
    {
        private readonly DataContext _dataContext;
        private readonly IMapper _mapper;

        public RidesRepository(DataContext dataContext, IMapper mapper)
        {
            _dataContext = dataContext;
            _mapper = mapper;
        }

        public async Task<Ride?> GetByIdAsync(bool withClients, bool withCars, int rideId)
        {
            var ridesQuery = _dataContext.Rides.AsQueryable();

            if (withClients) ridesQuery = ridesQuery.Include(x => x.Client);
            if (withCars) ridesQuery = ridesQuery.Include(x => x.Car);

            var ride = await ridesQuery.AsNoTracking()
                .FirstOrDefaultAsync(x => x.Id == rideId);

            return _mapper.Map<Ride?>(ride);
        }

        public async Task<IReadOnlyCollection<Ride>> GetAsync(bool withClients, bool withCars, int skip, int count)
        {
            var ridesQuery = _dataContext.Rides.AsQueryable();

            if (withClients) ridesQuery = ridesQuery.Include(x => x.Client);
            if (withCars) ridesQuery = ridesQuery.Include(x => x.Car);

            var rides = await ridesQuery.AsNoTracking()
                .Skip(skip)
                .Take(count)
                .ToArrayAsync();

            return _mapper.Map<IReadOnlyCollection<Ride>>(rides);
        }

        public async Task<IReadOnlyCollection<Ride>> GetForClientAsync(bool withCars, int clientId, int skip, int count)
        {
            var ridesQuery = _dataContext.Rides.AsQueryable();

            if (withCars) ridesQuery = ridesQuery.Include(x => x.Car);

            var rides = await ridesQuery.AsNoTracking()
                .Where(x => x.ClientId == clientId)
                .Skip(skip)
                .Take(count)
                .ToArrayAsync();

            return _mapper.Map<IReadOnlyCollection<Ride>>(rides);
        }

        public async Task<IReadOnlyCollection<Ride>> GetForCarAsync(bool withClients, int carId, int skip, int count)
        {
            var ridesQuery = _dataContext.Rides.AsQueryable();

            if (withClients) ridesQuery = ridesQuery.Include(x => x.Client);

            var rides = await ridesQuery.AsNoTracking()
                .Where(x => x.CarId == carId)
                .Skip(skip)
                .Take(count)
                .ToArrayAsync();

            return _mapper.Map<IReadOnlyCollection<Ride>>(rides);
        }

        public async Task<int> CreateNewRideAsync(Ride newRide)
        {
            if (newRide.Client is null
                || newRide.Car is null)
            {
                throw new InvalidOperationException("To create new ride Client and Car properties must be set.");
            }

            var entry = _dataContext.Rides.Add(new Data.Model.Ride
            {
                ClientId = newRide.Client.Id,
                CarId = newRide.Car.Id,
                Status = _mapper.Map<Data.Model.RideStatus>(newRide.Status)
            });

            await _dataContext.SaveChangesAsync();

            return entry.Entity.Id;
        }
    }
}
