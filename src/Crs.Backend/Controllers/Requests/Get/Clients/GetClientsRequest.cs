namespace Crs.Backend.Controllers.Requests.Get.Clients
{
    public sealed class GetClientsRequest : GetClientRequestBase
    {
        public int? Skip { get; set; }

        public int Count { get; set; }
    }
}
