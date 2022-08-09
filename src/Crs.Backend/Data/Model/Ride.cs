using System;

namespace Crs.Backend.Data.Model
{
    public class Ride
    {
        public int Id { get; set; }

        public int CarId { get; set; }

        public Car Car { get; set; }

        public int ClientId { get; set; }

        public Client Client { get; set; }

        public DateTime StartTime { get; set; }

        public DateTime? EndTime { get; set; }

        public double? Mileage { get; set; }

        public RideStatus Status { get; set; }
    }
}
