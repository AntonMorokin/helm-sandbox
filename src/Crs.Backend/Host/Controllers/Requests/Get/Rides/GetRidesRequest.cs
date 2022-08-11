namespace Crs.Backend.Host.Controllers.Requests.Get.Rides
{
    public sealed class GetRidesRequest : GetRideRequestBase
    {
        public int? Skip { get; set; }

        public int Count { get; set; }
    }
}
