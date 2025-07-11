using ControllerBase = Microsoft.AspNetCore.Mvc.ControllerBase;

namespace OrderUp_API.Controllers
{
    [ApiController]
    [Route("api/v1/health")]
    public class HealthCheckController : ControllerBase
    {
        [HttpHead]
        public IActionResult HealthCheck()
        {
            return Ok(new { status = "healthy", timestamp = DateTime.UtcNow });
        }
    }
}
