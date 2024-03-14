using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;

namespace OrderUp_API.Hubs {
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public class ServerHub : Hub {

        private readonly OnlineRestaurantDb onlineRestaurantDb;

        public ServerHub(OnlineRestaurantDb onlineRestaurantDb) {
            this.onlineRestaurantDb = onlineRestaurantDb;
        }

        public override async Task OnConnectedAsync() {

            onlineRestaurantDb.addRestaurant(Context.UserIdentifier);
            await base.OnConnectedAsync();

        }

        public override async Task OnDisconnectedAsync(Exception e) {

            onlineRestaurantDb.removeRestaurant(Context.UserIdentifier);
            await base.OnDisconnectedAsync(e);

        }

        public async Task Ping(string receiver, string tableNumber) {

            string sender = Context.UserIdentifier;

            string parsedRestaurantID = RestaurantIdentifier.ParseRestaurantName(receiver);

            if (onlineRestaurantDb.isOnline(parsedRestaurantID))

                await Clients.User(parsedRestaurantID).SendAsync("ping", $"User ${sender} at table ${tableNumber} needs your assistance");
        }

        public async Task AcknowledgePing(string receiver) {

            await Clients.User(receiver).SendAsync("ping", false);
        }

        public async Task ViewOnlineRestaurants() {
            await Clients.All.SendAsync("ping", onlineRestaurantDb.GetOnlineRestaurants());
        }

        public async Task SendToAll(string message) {

            await Clients.All.SendAsync("ReceiveMessage", message);
            //await Clients.AllExcept(Context.ConnectionId).SendAsync("ReceiveMessage", "Other user", message);
        }

        public async Task sendToRestaurant(string receiver, string message) {

            string sender = Context.UserIdentifier;

            await Clients.User(receiver).SendAsync("ReceiveMessage", message);
            //await Clients.AllExcept(Context.ConnectionId).SendAsync("ReceiveMessage", "Other user", message);
        }
    }
}
