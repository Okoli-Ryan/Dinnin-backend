﻿namespace OrderUp_API.Repository {
    public class RestaurantRepository : AbstractRepository<Restaurant> {

        public RestaurantRepository(OrderUpDbContext context) : base(context) { }

        public async Task<Restaurant> GetRestuarantDetailsBySlug(string Slug) {
            return await context.Restaurants.Where(x => x.Slug.Equals(Slug)).Include(x => x.MenuCategories).ThenInclude(x => x.MenuItems).AsNoTracking().FirstOrDefaultAsync();
        }

        public async Task<bool> DoesSlugExist(string Slug) {
            return await context.Restaurants.AnyAsync(x => x.Slug.Equals(Slug));
        }

        public Task<Restaurant> GetOneRestaurant()
        {
            return context.Restaurants.Take(1).FirstOrDefaultAsync();
        }
    }
}
