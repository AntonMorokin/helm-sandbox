using Crs.Backend.Model;
using System.Threading.Tasks;

namespace Crs.Backend.Logic.Repositories.Interfaces
{
    public interface IClientsRepository
    {
        Task<Client> GetByIdAsync(int clientId);

        Task<Client> GetByNameAsync(string firstName, string lastName);

        Task<int> AddNewClientAsync(Client newClient);
    }
}
