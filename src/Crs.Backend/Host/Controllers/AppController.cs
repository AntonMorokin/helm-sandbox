using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Reflection;

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

        [HttpGet("[action]")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(string))]
        public string Version()
        {
            return Assembly.GetExecutingAssembly().GetName().Version!.ToString(3);
        }
    }
}
