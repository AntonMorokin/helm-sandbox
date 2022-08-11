using System;

namespace Crs.Backend.Model
{
    public class Ride
    {
        public int Id { get; set; }

        public Car? Car { get; set; }

        public Client? Client { get; set; }

        public DateTime? StartTime { get; set; }

        public DateTime? EndTime { get; set; }

        public double? Mileage { get; set; }

        public RideStatus Status { get; set; }

        public Ride(Car? car, Client? client)
        {
            Car = car;
            Client = client;
            Status = RideStatus.Created;
        }

        public void Start()
        {
            if (Car is null
                || Client is null)
            {
                throw new InvalidOperationException("To start ride Car and Client properties must be set.");
            }

            StartTime = DateTime.Now;

            Status = RideStatus.InProgress;
        }

        public void Finish(double mileage)
        {
            if (Status != RideStatus.InProgress)
            {
                throw new InvalidOperationException("The ride has not been started yet.");
            }

            Status = RideStatus.Finished;

            EndTime = DateTime.Now;

            Mileage = mileage;
            Car!.Mileage += mileage;
        }

        public void Cancel()
        {
            Status = RideStatus.Canceled;

            EndTime = DateTime.Now;
        }
    }
}
