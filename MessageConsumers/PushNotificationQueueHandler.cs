using Mailjet.Client.Resources;
using Newtonsoft.Json;
using OrderUp_API.Interfaces;

namespace OrderUp_API.MessageConsumers {
    public class PushNotificationQueueHandler<T> : IQueueHandler where T : PushNotificationBody {

        readonly AdminService adminService;
        readonly PushNotificationService pushNotificationService;

        public PushNotificationQueueHandler(AdminService adminService, PushNotificationService pushNotificationService) {
            this.adminService = adminService;
            this.pushNotificationService = pushNotificationService;
        }


        public async Task HandleMessageAsync(string Message) {

            var PushNotificationPayload = JsonConvert.DeserializeObject<T>(Message);


            var Recipients = await adminService.GetAuthorizedPushNotificationRecipients(PushNotificationPayload.RestaurantID);

            await pushNotificationService.PublishToUsers(Recipients, PushNotificationPayload.Message);

        }
    }

    public class PushNotificationBody {

        public Guid RestaurantID { get; set; }

        public PushNotificationMessage Message { get; set; }
    }
}
