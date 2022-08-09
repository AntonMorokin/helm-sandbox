using Crs.Backend.Model;
using System.Threading.Tasks;

namespace Crs.Backend.Logic.Repositories.Interfaces
{
    public interface ICarsRepository
    {
        Task<Car> GetByIdAsync(int carId);

        Task<Car> GetByNumberAsync(string carNumber);
    }
}
