using PusherServer;

namespace OrderUp_API.Services {
    public class PushNotificationService {

        readonly PushNotifications pushNotifications;
        readonly HttpContext context;

        public PushNotificationService(IHttpContextAccessor contextAccessor) {
            var pushNotificationsOptions = new PushNotificationsOptions {
                InstanceId = ConfigurationUtil.GetConfigurationValue("Pusher-Beams:INSTANCE_ID"),
                SecretKey = ConfigurationUtil.GetConfigurationValue("Pusher-Beams:SECRET_KEY")
            };

            pushNotifications = new PushNotifications(new HttpClient(), pushNotificationsOptions);
            context = contextAccessor.HttpContext;
        }


        public DefaultResponse<string> GenerateToken() {

            var UserID = GetJwtValue.GetTokenFromCookie(context, ClaimTypes.PrimarySid);

            if (string.IsNullOrEmpty(UserID)) return new DefaultErrorResponse<string> {
                ResponseCode = ResponseCodes.UNAUTHORIZED,
                ResponseMessage = ResponseMessages.UNAUTHORIZED,
                ResponseData = null
            };

            var token = pushNotifications.GenerateToken(UserID);

            return new DefaultSuccessResponse<string>(token);

        }
    }
}
