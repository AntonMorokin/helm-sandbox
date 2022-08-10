﻿namespace Crs.Backend.Controllers.Requests
{
    public sealed class CreateNewCarRequest
    {
        public string Number { get; set; }

        public string Brand { get; set; }

        public string Model { get; set; }

        public double Mileage { get; set; }
    }
}
