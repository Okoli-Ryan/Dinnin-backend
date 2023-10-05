namespace OrderUp_API.DTOs {
    public class OrderDto : AbstractDto {

        public string orderNote { get; set; }

        public decimal price { get; set; }

        public Guid restaurantId { get; set; }

        public Guid userId { get; set; }

        public Guid tableId { get; set; }

        public string paymentOption { get; set; }

        public string orderStatus { get; set; }

        public List<OrderItemDto> orderItems { get; set; }

    }
}
