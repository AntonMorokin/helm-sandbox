using System.Collections.Generic;

namespace Crs.Backend.Model
{
    public class Car
    {
        public int Id { get; private set; }

        public string Number { get; set; }

        public string Brand { get; set; }

        public string Model { get; set; }

        public double Mileage { get; set; }

        public ICollection<Ride> Rides { get; } = new List<Ride>(8);

        public Car(int id, string number, string brand, string model)
        {
            Id = id;
            Number = number;
            Brand = brand;
            Model = model;
        }
    }
}
