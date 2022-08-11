using Microsoft.AspNetCore.Mvc;

namespace Crs.Backend.Host.Controllers.Requests.Update.Rides
{
    public sealed class FinishRideRequest
    {
        [FromRoute]
        public int Id { get; set; }

        public double Mileage { get; set; }
    }
}
