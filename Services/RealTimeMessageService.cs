using PusherServer;

namespace OrderUp_API.Services {
    public class RealTimeMessageService {

        readonly Pusher PusherService;

        public RealTimeMessageService() {

            string AppID = ConfigurationUtil.GetConfigurationValue("Pusher:APP_ID");
            string AppKey = ConfigurationUtil.GetConfigurationValue("Pusher:APP_KEY");
            string AppSecret = ConfigurationUtil.GetConfigurationValue("Pusher:APP_SECRET");
            string Cluster = ConfigurationUtil.GetConfigurationValue("Pusher:APP_CLUSTER");

            PusherService = new Pusher(AppID, AppKey, AppSecret, new PusherOptions() { Cluster = Cluster});
        }

        public async Task<object> SendData<T>(string Channel, string Event, T Data) {

            var Result = await PusherService.TriggerAsync(Channel, Event, Data);

            return Result;
        }
    }
}
