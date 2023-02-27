namespace OrderUp_API.Repository {
    public class MenuItemRepository : AbstractRepository<MenuItem> {

        public MenuItemRepository(OrderUpDbContext context) : base(context) { }

        public async Task<List<MenuItem>> GetMenuItemsByRestaurantID(Guid RestaurantID) {

            return await context.MenuItems.Where(x => x.RestaurantId.Equals(RestaurantID)).ToListAsync();
        }
    }
}
