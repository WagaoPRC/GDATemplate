using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace GDATemplate.Api.Controllers
{
    [AllowAnonymous]
    public class HealthController : Controller
    {
        [Route("health")]
        [HttpGet]
        public IActionResult Status()
        {
            return Ok("Healthy");
        }
    }
}