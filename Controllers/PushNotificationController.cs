using Microsoft.AspNetCore.Mvc;
using Pusher.PushNotifications;

namespace OrderUp_API.Controllers {
    [Route("api/v1/pusher")]
    [ApiController]
    public class PushNotificationController : ControllerBase {

        readonly PushNotificationService pushNotification;
        readonly ControllerResponseHandler responseHandler;

        public PushNotificationController(PushNotificationService pushNotification) {
            this.pushNotification = pushNotification;
            responseHandler = new ControllerResponseHandler();
        }

        [HttpGet("gen-token")]
        public IActionResult GenerateToken([FromQuery(Name = "user_id")] string UserID) {

            var tokenResponse = pushNotification.GenerateToken(UserID);

            return responseHandler.HandleResponse(tokenResponse);

        }
    }
}
