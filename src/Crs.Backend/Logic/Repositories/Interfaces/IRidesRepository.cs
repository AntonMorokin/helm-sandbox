using Crs.Backend.Model;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Crs.Backend.Logic.Repositories.Interfaces
{
    public interface IRidesRepository
    {
        Task<IReadOnlyCollection<Ride>> GetByPeriodAsync(DateTime startDate, DateTime endDate);
    }
}
