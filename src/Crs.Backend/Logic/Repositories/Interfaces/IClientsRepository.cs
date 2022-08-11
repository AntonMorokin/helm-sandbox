using Crs.Backend.Model;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Crs.Backend.Logic.Repositories.Interfaces
{
    public interface IClientsRepository
    {
        Task<IReadOnlyCollection<Client>> GetAsync(int skip, int count);

        Task<Client?> GetByIdAsync(int clientId);

        Task<Client?> GetByNameAsync(string firstName, string lastName);

        Task<int> CreateNewClientAsync(Client newClient);
    }
}
