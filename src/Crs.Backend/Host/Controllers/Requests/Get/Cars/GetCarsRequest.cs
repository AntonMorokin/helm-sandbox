namespace Crs.Backend.Host.Controllers.Requests.Get.Cars
{
    public sealed class GetCarsRequest
    {
        public int? Skip { get; set; }

        public int Count { get; set; }
    }
}
