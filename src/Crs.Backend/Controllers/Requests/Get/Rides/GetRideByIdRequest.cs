using Microsoft.AspNetCore.Mvc;

namespace Crs.Backend.Controllers.Requests.Get.Rides
{
    public sealed class GetRideByIdRequest : GetRideRequestBase
    {
        [FromRoute]
        public int Id { get; set; }
    }
}
