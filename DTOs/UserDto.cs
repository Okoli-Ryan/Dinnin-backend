namespace OrderUp_API.DTOs {
    public class UserDto : IUserEntityDto {

        public string firstName { get; set; }

        public string lastName { get; set; }

        public string phoneNumber { get; set; }

        public string imageUrl { get; set; }

        public virtual List<SavedRestaurantDto> savedRestaurants { get; set; }
    }
}
