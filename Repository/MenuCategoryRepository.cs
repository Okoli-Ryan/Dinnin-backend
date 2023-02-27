namespace OrderUp_API.Repository {
    public class MenuCategoryRepository : AbstractRepository<MenuCategory> {

        public MenuCategoryRepository(OrderUpDbContext context) : base(context) { }
    }
}
