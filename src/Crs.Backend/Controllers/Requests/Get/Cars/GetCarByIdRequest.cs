using Microsoft.AspNetCore.Mvc;

namespace Crs.Backend.Controllers.Requests.Get.Cars
{
    public sealed class GetCarByIdRequest
    {
        [FromRoute]
        public int Id { get; set; }
    }
}
