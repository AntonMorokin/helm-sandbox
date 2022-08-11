using Microsoft.AspNetCore.Mvc;

namespace Crs.Backend.Host.Controllers.Requests.Get.Rides
{
    public sealed class GetRidesForClientRequest
    {
        public bool WithCars { get; set; }

        [FromRoute]
        public int ClientId { get; set; }

        public int? Skip { get; set; }

        public int Count { get; set; }
    }
}
