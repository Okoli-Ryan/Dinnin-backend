namespace OrderUp_API.Repository {
    public class SideOrderItemRepository : AbstractRepository<SideOrderItem> {

        public SideOrderItemRepository(OrderUpDbContext context) : base(context) { }
    }
}
