using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Crs.Backend.Host.Controllers
{
    [ApiController]
    public sealed class AppController : ControllerBase
    {
        [HttpGet("[action]")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public IActionResult Health()
        {
            return Ok();
        }

        [HttpGet("[action]")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public IActionResult Ready()
        {
            return Ok();
        }
    }
}
