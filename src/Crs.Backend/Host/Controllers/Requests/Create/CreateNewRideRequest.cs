namespace Crs.Backend.Host.Controllers.Requests.Create
{
    public sealed class CreateNewRideRequest
    {
        public int ClientId { get; set; }

        public int CarId { get; set; }
    }
}
