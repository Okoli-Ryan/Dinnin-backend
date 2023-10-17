using Microsoft.AspNetCore.Mvc;
using Pusher.PushNotifications;

namespace OrderUp_API.Controllers {
    [Route("pusher")]
    [ApiController]
    public class PushNotificationController : ControllerBase {

        readonly PushNotificationService pushNotification;
        readonly ControllerResponseHandler responseHandler;

        public PushNotificationController(PushNotificationService pushNotification) {
            this.pushNotification = pushNotification;
            responseHandler = new ControllerResponseHandler();
        }

        [HttpGet("gen-token")]
        public IActionResult GenerateToken() {

            var tokenResponse = pushNotification.GenerateToken();

            return responseHandler.HandleResponse(tokenResponse);

        }
    }
}
