namespace OrderUp_API.DTOs {
    public class AdminDto : IUserEntityDto {

        public string firstName { get; set; }

        public string lastName { get; set; }

        public string phoneNumber { get; set; }

        public string imageUrl { get; set; }

        public Guid? restaurantId { get; set; }

        public string role { get; set; }

        public RestaurantDto? restaurant { get; set; }
    }
}
