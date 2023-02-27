namespace OrderUp_API.Repository {
    public class MenuItemImageRepository : AbstractRepository<MenuItemImage> {

        public MenuItemImageRepository(OrderUpDbContext context) : base(context) { }
    }
}
