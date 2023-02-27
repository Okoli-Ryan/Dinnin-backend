namespace OrderUp_API.Classes {
    public class MakeOrder {

        public OrderDto Order { get; set; }

        public List<OrderItemDto> OrderItems { get; set; }
    }
}
