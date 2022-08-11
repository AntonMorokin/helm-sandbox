using Microsoft.AspNetCore.Mvc;

namespace Crs.Backend.Host.Controllers.Requests.Update.Rides
{
    public sealed class CancelRideRequest
    {
        [FromRoute]
        public int Id { get; set; }
    }
}
