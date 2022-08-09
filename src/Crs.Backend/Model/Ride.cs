using System;

namespace Crs.Backend.Model
{
    public class Ride
    {
        public int Id { get; private set; }

        public Car Car { get; set; }

        public Client Client { get; set; }

        public DateTime StartTime { get; set; }

        public DateTime? EndTime { get; set; }

        public double? Mileage { get; set; }

        public RideStatus Status { get; set; }

        public Ride(int id, Car car, Client client)
        {
            Id = id;
            Car = car;
            Client = client;
            Status = RideStatus.Created;

            Client.Rides.Add(this);
            Car.Rides.Add(this);
        }

        public void Start()
        {
            StartTime = DateTime.Now;

            Status = RideStatus.InProgress;
        }

        public void Finish(double mileage)
        {
            Status = RideStatus.Finished;

            EndTime = DateTime.Now;

            Mileage = mileage;
            Car.Mileage += mileage;
        }
    }
}
