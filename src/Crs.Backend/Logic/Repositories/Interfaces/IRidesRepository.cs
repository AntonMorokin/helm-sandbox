using Crs.Backend.Model;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Crs.Backend.Logic.Repositories.Interfaces
{
    public interface IRidesRepository
    {
        Task<IReadOnlyCollection<Ride>> GetAsync(bool withClients, bool withCars, int skip, int count);

        Task<IReadOnlyCollection<Ride>> GetForClientAsync(bool withCars, int clientId, int skip, int count);

        Task<IReadOnlyCollection<Ride>> GetForCarAsync(bool withClients, int carId, int skip, int count);

        Task<Ride?> GetByIdAsync(bool withClients, bool withCars, int rideId);

        Task<int> CreateNewRideAsync(Ride newRide);
    }
}
