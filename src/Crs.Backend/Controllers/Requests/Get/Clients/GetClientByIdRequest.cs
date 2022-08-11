using Microsoft.AspNetCore.Mvc;
using System.Text.Json.Serialization;

namespace Crs.Backend.Controllers.Requests.Get.Clients
{
    public sealed class GetClientByIdRequest : GetClientRequestBase
    {
        [FromRoute]
        public int Id { get; set; }
    }
}
