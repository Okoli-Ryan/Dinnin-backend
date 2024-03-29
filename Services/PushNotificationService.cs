﻿using Pusher.PushNotifications;

namespace OrderUp_API.Services {
    public class PushNotificationService {

        readonly PushNotifications pushNotifications;
        readonly HttpContext context;

        public PushNotificationService(IHttpContextAccessor contextAccessor) {
            var pushNotificationsOptions = new PushNotificationsOptions {
                InstanceId = ConfigurationUtil.GetConfigurationValue("Pusher_Beams_INSTANCE_ID"),
                SecretKey = ConfigurationUtil.GetConfigurationValue("Pusher_Beams_SECRET_KEY")
            };

            pushNotifications = new PushNotifications(new HttpClient(), pushNotificationsOptions);
            context = contextAccessor.HttpContext;
        }


        public DefaultResponse<object> GenerateToken(string UserIDFromQuery) {

            var UserID = GetJwtValue.GetTokenFromCookie(context, ClaimTypes.PrimarySid);

            if (string.IsNullOrEmpty(UserID) || !UserIDFromQuery.Equals(UserID)) return new DefaultErrorResponse<object> {
                ResponseCode = ResponseCodes.UNAUTHORIZED,
                ResponseMessage = ResponseMessages.UNAUTHORIZED,
                ResponseData = null
            };

            var token = pushNotifications.GenerateToken(UserID);

            return new DefaultSuccessResponse<object>(new { token });

        }


        public async Task<string> PublishToUsers<T>(List<T> Users, PushNotificationMessage message) where T : IUserEntity {

            var UserIDs = Users.Select(x => x.ID.ToString()).ToList();

            var pushMesage = new Dictionary<string, object>() {
                ["web"] = new Dictionary<string, object>() {
                    ["notification"] = message
                }
            };


            var response = await pushNotifications.PublishToUsers(UserIDs, pushMesage);

            return response;

        }

    }
}

public class PushNotificationMessage {

    public string Title { get; set; }
    public string Body { get; set; }
}
