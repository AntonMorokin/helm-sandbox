using System.Collections.Generic;

namespace Crs.Backend.Model
{
    public class Car
    {
        public int Id { get; set; }

        public string Number { get; set; }

        public string Brand { get; set; }

        public string Model { get; set; }

        public double Mileage { get; set; }

        public Car(string number, string brand, string model, double mileage)
        {
            Number = number;
            Brand = brand;
            Model = model;
            Mileage = mileage;
        }
    }
}
