namespace OrderUp_API.Constants {
    public class RestaurantIdentifier {

        public const string RestaurantPrefix = "Restaurant___";
        public const string RestaurantClaimType = "RestaurantID";

        public static bool IsRestaurant(string restaurantIdentifier) {
            return restaurantIdentifier.StartsWith(RestaurantPrefix);
        }

        public static string ParseRestaurantName(string restaurantIdentifier) {
            return $"{RestaurantPrefix}{restaurantIdentifier}";
        }

        public static string GetRestaurantIdentifier(string parsedRestaurantName) {
            string[] strings = parsedRestaurantName.Split(RestaurantPrefix);

            if (strings.Length == 2) {
                return strings[1];
            }

            return "";
        }
    }
}
