using Microsoft.AspNetCore.Mvc;

namespace Crs.Backend.Host.Controllers.Requests.Get.Clients
{
    public sealed class GetClientByIdRequest
    {
        [FromRoute]
        public int Id { get; set; }
    }
}
