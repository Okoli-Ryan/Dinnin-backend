using OrderUp_API.Classes.AnalyticsModels;

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
                               .Where(x => x.RestaurantId.Equals(RestaurantID) && x.ActiveStatus);

            if (LastTime.HasValue) {
                query = query.Where(x => x.CreatedAt > LastTime.Value);
            }

            return await query.Include(x => x.Table)
                              .Include(o => o.OrderItems)
                              .OrderByDescending(x => x.CreatedAt)
                              .AsNoTracking()
                              .ToListAsync();
        }



        public async Task<List<OrderAmountAnalytics>> GetOrdersByDate(DateTime? StartTime, DateTime EndTime) {

            StartTime = StartTime.HasValue ? StartTime.Value : DateTime.Now;

            return await context.Order.Where(x => StartTime > x.CreatedAt && x.CreatedAt < EndTime).Select(x => new OrderAmountAnalytics { CreatedAt = x.CreatedAt, Amount = x.OrderAmount ?? 0 }).ToListAsync();
        }
        
    }
}
