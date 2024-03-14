namespace OrderUp_API.DTOs {
    public class SavedRestaurantDto : AbstractDto {

        public Guid restaurantId { get; set; }

        public Guid userId { get; set; }

        public RestaurantDto restaurant { get; set; }

        public UserDto user { get; set; }
    }
}
