namespace Crs.Backend.Host.Controllers.Requests.Get.Rides
{
    public abstract class GetRideRequestBase
    {
        public bool WithClients { get; set; }

        public bool WithCars { get; set; }
    }
}
