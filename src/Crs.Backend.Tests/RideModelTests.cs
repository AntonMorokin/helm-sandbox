using Crs.Backend.Model;
using NUnit.Framework;
using System;

namespace Crs.Backend.Tests
{
    // Tests just for lulz (and learning/demo purpose of course)
    public class RideModelTests
    {
        [Test]
        public void Ride_GetsCreatedStatus_WhenNewInstanceIsCreated()
        {
            var ride = new Ride(null, null);

            Assert.That(ride.Status, Is.EqualTo(RideStatus.Created));
        }

        [Test]
        public void Ride_GetsInProgressStatus_WhenRideIsStarted()
        {
            var car = new Car("abc", "toyota", "camry", 12345);
            var client = new Client("ivan", "petrov", new DateOnly(1990, 1, 1));
            var ride = new Ride(car, client);

            var calledDateTime = DateTime.Now;
            ride.Start();

            Assert.Multiple(() =>
            {
                Assert.That(ride.Status, Is.EqualTo(RideStatus.InProgress));
                Assert.That(ride.StartTime, Is.EqualTo(calledDateTime).Within(TimeSpan.FromSeconds(1)));
            });
        }

        [Test]
        public void Ride_GetsFinishedStatus_WhenRideIsFinished()
        {
            var car = new Car("abc", "toyota", "camry", 12345);
            var client = new Client("ivan", "petrov", new DateOnly(1990, 1, 1));
            var ride = new Ride(car, client);

            var calledDateTime = DateTime.Now;
            ride.Start();
            ride.Finish(123);

            Assert.Multiple(() =>
            {
                Assert.That(ride.Status, Is.EqualTo(RideStatus.Finished));
                Assert.That(ride.EndTime, Is.EqualTo(calledDateTime).Within(TimeSpan.FromSeconds(1)));
                Assert.That(ride.Mileage, Is.EqualTo(123));
                Assert.That(car.Mileage, Is.EqualTo(12345 + 123));
            });
        }
    }
}