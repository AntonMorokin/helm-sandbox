using Crs.Backend.Model;
using System;

namespace Crs.Backend.Host.Controllers.Responses
{
    public sealed class RideResponse
    {
        public int Id { get; set; }

        public ClientResponse? Client { get; set; }

        public CarResponse? Car { get; set; }

        public DateTime? StartTime { get; set; }

        public DateTime? EndTime { get; set; }

        public double? Mileage { get; set; }

        public RideStatus Status { get; set; }
    }
}
