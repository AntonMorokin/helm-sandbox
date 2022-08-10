namespace Crs.Backend.Controllers.Responses
{
    public sealed class CarResponse
    {
        public int Id { get; set; }

        public string Number { get; set; }

        public string Brand { get; set; }

        public string Model { get; set; }

        public double Mileage { get; set; }
    }
}
