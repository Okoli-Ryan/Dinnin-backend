using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace OrderUp_API.Controllers {
    [Route("api/v1/[controller]")]
    [ApiController]
    public class TestController : ControllerBase {

        readonly RealTimeMessageService messageService;

        public TestController(RealTimeMessageService messageService) {
            this.messageService = messageService;
        }

        [HttpGet]
        public async Task<IActionResult> SendPusherMessage() {

            var result = await messageService.SendData("Channel", "Event", new { message = "Received" });

            return Ok(result);
        }
    }
}
