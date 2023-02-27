namespace OrderUp_API.Repository {
    public class SideItemRepository : AbstractRepository<SideItem> {

        public SideItemRepository(OrderUpDbContext context) : base(context) { }
    }
}
