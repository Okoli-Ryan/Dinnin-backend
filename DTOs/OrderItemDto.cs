namespace OrderUp_API.DTOs {
    public class OrderItemDto : AbstractDto {

        public int quantity { get; set; }

        public Guid menuItemId { get; set; }

        public MenuItemDto menuItem { get; set; }

        public Guid orderId { get; set; }

        public OrderDto order { get; set; }
    }
}
