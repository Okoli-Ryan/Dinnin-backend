namespace OrderUp_API.Repository {
    public class SidesOrderRepository : AbstractRepository<SidesOrder> {

        public SidesOrderRepository(OrderUpDbContext context) : base(context) { }
    }
}
