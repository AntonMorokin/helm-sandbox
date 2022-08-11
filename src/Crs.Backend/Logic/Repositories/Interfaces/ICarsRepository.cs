using Crs.Backend.Model;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Crs.Backend.Logic.Repositories.Interfaces
{
    public interface ICarsRepository
    {
        Task<IReadOnlyCollection<Car>> GetAsync(int skip, int count);

        Task<Car?> GetByIdAsync(int carId);

        Task<Car?> GetByNumberAsync(string carNumber);

        Task<int> CreateNewCarAsync(Car newCar);
    }
}
