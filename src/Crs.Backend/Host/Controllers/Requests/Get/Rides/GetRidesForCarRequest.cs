using Microsoft.AspNetCore.Mvc;

namespace Crs.Backend.Host.Controllers.Requests.Get.Rides
{
    public sealed class GetRidesForCarRequest
    {
        public bool WithClients { get; set; }

        [FromRoute]
        public int CarId { get; set; }

        public int? Skip { get; set; }

        public int Count { get; set; }
    }
}
