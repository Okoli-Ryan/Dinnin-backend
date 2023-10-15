using Microsoft.AspNetCore.Mvc;
using Pusher.PushNotifications;

namespace OrderUp_API.Controllers {
    [Route("pusher")]
    [ApiController]
    public class PushNotificationController : ControllerBase {

        readonly PushNotifications pushNotifications;

        public PushNotificationController() {
            var pushNotificationsOptions = new PushNotificationsOptions {
                InstanceId = "YOUR_INSTANCE_ID_HERE",
                SecretKey = "YOUR_SECRET_KEY_HERE"
            };

            pushNotifications = new PushNotifications(new HttpClient(), pushNotificationsOptions);
        }

        [HttpGet]
        public async Task<IActionResult> GenerateToken() {

            var token = pushNotifications.GenerateToken();

        }
    }
}
