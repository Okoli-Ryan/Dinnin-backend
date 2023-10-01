namespace OrderUp_API.DTOs {
    public class TableDto : AbstractDto {

        public string tableName { get; set; }

        public string code { get; set; }

        public Guid restaurantId { get; set; }

        public RestaurantDto restaurant { get; set; }
    }
}
