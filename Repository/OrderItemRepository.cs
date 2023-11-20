using OrderUp_API.Classes.AnalyticsModels;

namespace OrderUp_API.Repository {
    public class OrderItemRepository : AbstractRepository<OrderItem>{

        public OrderItemRepository(OrderUpDbContext context) : base(context) { }

        public async Task<List<OrderItemAnalyticsData>> GetOrderItemCountAnalytics(Guid? RestaurantID, DateTime StartTime, DateTime EndTime) {

            return await context.OrderItem
                                .Include(o => o.MenuItem)
                                .Where(x => x.MenuItem.RestaurantId == RestaurantID && x.CreatedAt >= StartTime && x.CreatedAt < EndTime)
                                .GroupBy(x => x.MenuItemName)
                                .Select(g => new OrderItemAnalyticsData {
                                    ItemName = g.Key,
                                    Count = g.Sum(x => x.Quantity)
                                })
                                .AsNoTracking()
                                .ToListAsync();
        }
    }
}
