using System.Collections.Generic;

namespace Crs.Backend.Data.Model
{
    public class Car
    {
        public int Id { get; set; }

        public string Number { get; set; }

        public string Brand { get; set; }

        public string Model { get; set; }

        public double Mileage { get; set; }

        public List<Ride> Rides { get; set; }
    }
}
