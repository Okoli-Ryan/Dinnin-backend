namespace OrderUp_API.Repository {
    public class MenuCategoryRepository : AbstractRepository<MenuCategory> {

        public MenuCategoryRepository(OrderUpDbContext context) : base(context) { }

        public List<MenuCategory> GetMenuCategoryByRestaurantID(Guid RestaurantID) {

            return context.MenuCategory.Where(x => x.RestaurantID.Equals(RestaurantID)).OrderBy(x => x.Order).Include(x => x.MenuItems).ToList();
        }

        public async Task<int> GetCount(Guid RestaurantID) {
            return await context.MenuCategory.Where(x => x.RestaurantID.Equals(RestaurantID)).CountAsync();
        }
    }
}
