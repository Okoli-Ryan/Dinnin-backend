namespace OrderUp_API.Repository {
    public class OrderItemRepository : AbstractRepository<OrderItem>{

        public OrderItemRepository(OrderUpDbContext context) : base(context) { }
    }
}
