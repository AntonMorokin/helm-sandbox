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
    internal sealed class ClientsRepository : IClientsRepository
    {
        private readonly DataContext _dataContext;
        private readonly IMapper _mapper;

        public ClientsRepository(DataContext dataContext, IMapper mapper)
        {
            _dataContext = dataContext;
            _mapper = mapper;
        }

        public async Task<IReadOnlyCollection<Client>> GetAsync(int skip, int count)
        {
            var clients = await _dataContext.Clients.AsNoTracking()
                .Skip(skip)
                .Take(count)
                .ToArrayAsync();

            return _mapper.Map<IReadOnlyCollection<Client>>(clients);
        }

        public async Task<Client?> GetByIdAsync(int clientId)
        {
            var client = await _dataContext.Clients.AsNoTracking()
                .FirstOrDefaultAsync(c => c.Id == clientId);

            return _mapper.Map<Client?>(client);
        }

        public async Task<Client?> GetByNameAsync(string firstName, string lastName)
        {
            // Or use raw SQL ILIKE in case of performance issues.
            var client = await _dataContext.Clients.AsNoTracking()
                .FirstOrDefaultAsync(
                    c => c.FirstName.ToUpper() == firstName.ToUpper()
                    && c.LastName.ToUpper() == lastName.ToUpper());

            return _mapper.Map<Client?>(client);
        }

        public async Task<int> CreateNewClientAsync(Client newClient)
        {
            var entry = _dataContext.Clients.Add(new Data.Model.Client
            {
                FirstName = newClient.FirstName,
                LastName = newClient.LastName,
                BirthDate = newClient.BirdthDate
            });

            await _dataContext.SaveChangesAsync();

            return entry.Entity.Id;
        }
    }
}
