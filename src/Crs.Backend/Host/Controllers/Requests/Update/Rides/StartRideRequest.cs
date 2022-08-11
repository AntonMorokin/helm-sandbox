using Microsoft.AspNetCore.Mvc;

namespace Crs.Backend.Host.Controllers.Requests.Update.Rides
{
    public sealed class StartRideRequest
    {
        [FromRoute]
        public int Id { get; set; }
    }
}
