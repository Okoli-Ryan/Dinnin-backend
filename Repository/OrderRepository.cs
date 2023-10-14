namespace OrderUp_API.Repository {
    public class OrderRepository : AbstractRepository<Order> {

        public OrderRepository(OrderUpDbContext context) : base(context) { }

        public async Task<List<Order>> GetOrdersByRestaurantID(Guid RestauantID, int Page) {

            int PageSize = 10;
            int SkipCount = (Page - 1) * PageSize;

            return await context.Order.Where(x => x.RestaurantId.Equals(RestauantID))
                                      .Include(o => o.OrderItems)
                                      .OrderByDescending(o => o.CreatedAt)
                                      .Skip(SkipCount)
                                      .Take(PageSize).AsNoTracking()
                                      .ToListAsync();
        }

        public async Task<List<Order>> GetActiveOrders(Guid RestaurantID, DateTime? LastTime = null) {
            var query = context.Order
                               .Where(x => x.RestaurantId.Equals(RestaurantID) && x.ActiveStatus && !x.OrderStatus.Equals(OrderModelConstants.COMPLETED));

            if (LastTime.HasValue) {
                query = query.Where(x => x.CreatedAt > LastTime.Value);
            }

            return await query.Include(x => x.Table)
                              .Include(o => o.OrderItems)
                              .OrderByDescending(x => x.CreatedAt)
                              .AsNoTracking()
                              .ToListAsync();
        }



    }
}
