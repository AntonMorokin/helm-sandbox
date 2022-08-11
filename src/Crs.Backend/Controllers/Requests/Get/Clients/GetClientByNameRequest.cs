namespace Crs.Backend.Controllers.Requests.Get.Clients
{
    public sealed class GetClientByNameRequest : GetClientRequestBase
    {
        public string FirstName { get; set; }

        public string LastName { get; set; }
    }
}
