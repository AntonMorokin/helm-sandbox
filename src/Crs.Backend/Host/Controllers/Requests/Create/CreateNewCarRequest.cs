namespace Crs.Backend.Host.Controllers.Requests.Create
{
    public sealed class CreateNewCarRequest
    {
        public string Number { get; set; }

        public string Brand { get; set; }

        public string Model { get; set; }

        public double Mileage { get; set; }
    }
}
