namespace OrderUp_API.Repository {
    public class OrderRepository : AbstractRepository<Order> {

        public OrderRepository(OrderUpDbContext context) : base(context) { }
    }
}
