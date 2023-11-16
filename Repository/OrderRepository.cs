using Microsoft.EntityFrameworkCore;
using OrderUp_API.Classes.AnalyticsModels;
using System.Data.Entity.Core.Objects;

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



        public async Task<List<ChartData<decimal>>> GetOrderAmountAnalytics(Guid? RestaurantID, DateTime StartTime, DateTime EndTime) {

            return await context.Order
                                .Where(x => x.RestaurantId == RestaurantID && x.CreatedAt >= StartTime && x.CreatedAt < EndTime)
                                .GroupBy(o => o.CreatedAt)
                                .Select(x => new ChartData<decimal> { Date = x.Key, Data = x.Sum(o => o.OrderAmount ?? 0) })
                                .OrderBy(o => o.Date)
                                .ToListAsync();
        }


        public async Task<List<ChartData<int>>> GetOrderCountAnalytics(Guid? RestaurantID, DateTime StartTime, DateTime EndTime) {

            return await context.Order
                                .Where(x => x.RestaurantId == RestaurantID && x.CreatedAt >= StartTime && x.CreatedAt < EndTime)
                                .GroupBy(o => o.CreatedAt)
                                .Select(x => new ChartData<int> { Date = x.Key, Data = x.Count() })
                                .OrderBy(o => o.Date)
                                .ToListAsync();
        }
    }
}
