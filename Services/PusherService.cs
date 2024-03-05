using PusherServer;

namespace OrderUp_API.Services {
    public class PusherService {
        readonly PusherServer.Pusher pusher;

        public PusherService() {

            var options = new PusherOptions() {
                Cluster = ConfigurationUtil.GetConfigurationValue("Pusher_APP_CLUSTER"),
                Encrypted = true
            };

            var Pusher_APP_ID = ConfigurationUtil.GetConfigurationValue("Pusher_APP_ID");
            var Pusher_APP_KEY = ConfigurationUtil.GetConfigurationValue("Pusher_APP_KEY");
            var Pusher_APP_SECRET = ConfigurationUtil.GetConfigurationValue("Pusher_APP_SECRET");

            var PusherInstance = new PusherServer.Pusher(Pusher_APP_ID, Pusher_APP_KEY, Pusher_APP_SECRET, options);

            pusher = PusherInstance;
        }

        public async Task<ITriggerResult> TriggerMessage<T>(T Message, string Event, string GroupID) {
            return await pusher.TriggerAsync(GroupID, Event, Message);
        }

    }
}
