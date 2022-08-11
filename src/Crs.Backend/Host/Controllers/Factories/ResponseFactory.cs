using Crs.Backend.Host.Controllers.Responses;
using Crs.Backend.Model;
using System;
using System.Linq;

namespace Crs.Backend.Host.Controllers.Factories
{
    internal static class ResponseFactory
    {
        private static readonly TimeOnly ZeroTime = TimeOnly.MinValue;

        public static ClientResponse CreateResponse(Client client)
            => new()
            {
                Id = client.Id,
                FirstName = client.FirstName,
                LastName = client.LastName,
                BirdthDate = client.BirdthDate.ToDateTime(ZeroTime)
            };

        public static CarResponse CreateResponse(Car car)
            => new()
            {
                Id = car.Id,
                Number = car.Number,
                Brand = car.Brand,
                Model = car.Model,
                Mileage = car.Mileage
            };

        public static RideResponse CreateResponse(Ride ride)
        {
            var obj = new RideResponse
            {
                Id = ride.Id,
                StartTime = ride.StartTime,
                EndTime = ride.EndTime,
                Mileage = ride.Mileage,
                Status = ride.Status
            };

            if (ride.Client is not null)
            {
                obj.Client = CreateResponse(ride.Client);
            }

            if (ride.Car is not null)
            {
                obj.Car = CreateResponse(ride.Car);
            }

            return obj;
        }
    }
}
