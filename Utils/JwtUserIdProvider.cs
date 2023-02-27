namespace OrderUp_API.Utils {
    public class JwtUserIdProvider : IUserIdProvider {

        public string GetUserId(HubConnectionContext connection) {

            string connectionId;


            Claim RestaurantClaim = connection.User?.FindFirst(RestaurantIdentifier.RestaurantClaimType);

            if(RestaurantClaim is not null) {

                connectionId = RestaurantIdentifier.ParseRestaurantName(RestaurantClaim.Value);
                return connectionId;
            }

            connectionId = connection.User?.FindFirst(x => x.Type == ClaimTypes.Email).Value;

            return connectionId;
        }
    }
}
