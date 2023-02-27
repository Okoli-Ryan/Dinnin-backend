namespace OrderUp_API.Repository {
    public class SavedRestaurantRepository : AbstractRepository<SavedRestaurant> {

        public SavedRestaurantRepository(OrderUpDbContext context) : base(context) { }
    }
}
