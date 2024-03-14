using System.Collections.Concurrent;

namespace OrderUp_API.Data {
    public class OnlineRestaurantDb {

        private readonly ConcurrentDictionary<string, int> OnlineRestaurants;

        public OnlineRestaurantDb() {
            this.OnlineRestaurants = new ConcurrentDictionary<string, int>();
        }

        public void addRestaurant(string RestaurantID) {

            if (!RestaurantIdentifier.IsRestaurant(RestaurantID)) return;

            bool restaurantPreviouslySigned = OnlineRestaurants.ContainsKey(RestaurantID);

            if (restaurantPreviouslySigned) {
                OnlineRestaurants.TryGetValue(RestaurantID, out int registeredRestaurantCount);

                OnlineRestaurants.TryUpdate(RestaurantID, registeredRestaurantCount + 1, registeredRestaurantCount);

            }
            OnlineRestaurants.TryAdd(RestaurantID, 1);
        }

        public void removeRestaurant(string RestaurantID) {
            int previousValue;
            OnlineRestaurants.TryGetValue(RestaurantID, out previousValue);

            OnlineRestaurants.TryUpdate(RestaurantID, previousValue - 1, previousValue);
        }

        public ConcurrentDictionary<string, int> GetOnlineRestaurants() {
            return OnlineRestaurants;
        }

        public bool isOnline(string RestaurantID) {

            int NumberOfOnlineRestaurantAdmins = 0;

            bool restautantIsOnline = OnlineRestaurants.TryGetValue(RestaurantID, out NumberOfOnlineRestaurantAdmins);
            return restautantIsOnline;
        }
    }
}
