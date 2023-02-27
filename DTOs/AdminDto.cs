namespace OrderUp_API.DTOs {
    public class AdminDto : UserDto{

        public Guid restaurantId { get; set; }

        public RestaurantDto? restaurant { get; set; }
    }
}
