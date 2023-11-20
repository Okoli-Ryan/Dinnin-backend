
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using OrderUp_API.Classes.AnalyticsModels;

namespace OrderUp_API.Services {
    public class AnalyticsService {

        private OrderRepository orderRepository;
        private OrderItemRepository orderItemRepository;
        private HttpContext httpContext;

        public AnalyticsService(OrderRepository orderRepository, OrderItemRepository orderItemRepository, IHttpContextAccessor httpContextAccessor) {
            this.orderRepository = orderRepository;
            this.orderItemRepository = orderItemRepository;
            httpContext = httpContextAccessor.HttpContext;
        }


        // Get Orders and Order Items in parallel


        public async Task<AnalyticsGrowth<int>> GetCompletedOrdersData(List<Order> Orders) {

            var response = new AnalyticsGrowth<int>();

            var currentDate = DateTime.Now;

            var lastMonthDate = DateTime.Now.AddMonths(-1);

            var currentMonthOrders = Orders.Where(o => o.CreatedAt >= lastMonthDate).ToList();

            var lastMonthOrders = Orders.Where(o => o.CreatedAt >= lastMonthDate && o.CreatedAt < lastMonthDate).ToList();

            response.Total = Orders.Count;

            response.PercentageChange = CalculatePercentageChange(currentMonthOrders.Count, lastMonthOrders.Count);

            return response;

        }


        public async Task<AnalyticsGrowth<int>> GetCompletedOrderItemsData(List<OrderItem> OrderItems) {

            var response = new AnalyticsGrowth<int>();

            var currentDate = DateTime.Now;

            var lastMonthDate = DateTime.Now.AddMonths(-1);

            var currentMonthOrders = OrderItems.Where(o => o.CreatedAt >= lastMonthDate).ToList();

            var lastMonthOrders = OrderItems.Where(o => o.CreatedAt >= lastMonthDate && o.CreatedAt < lastMonthDate).ToList();

            response.Total = OrderItems.Count;

            response.PercentageChange = CalculatePercentageChange(currentMonthOrders.Count, lastMonthOrders.Count);

            return response;

        }



        public async Task<AnalyticsGrowth<decimal>> GetTotalRevenueData(List<Order> Orders) {

            var response = new AnalyticsGrowth<decimal>();

            var currentDate = DateTime.Now;

            var lastMonthDate = DateTime.Now.AddMonths(-1);

            var currentMonthRevenue = Orders.Where(o => o.CreatedAt >= lastMonthDate).Sum(x => x.OrderAmount ?? 0);

            var lastMonthRevenue = Orders.Where(o => o.CreatedAt >= lastMonthDate && o.CreatedAt < lastMonthDate).Sum(x => x.OrderAmount ?? 0);

            response.Total = Orders.Sum(x => x.OrderAmount ?? 0);

            response.PercentageChange = CalculatePercentageChange(currentMonthRevenue, lastMonthRevenue);

            return response;

        }



        public async Task<List<ChartData<decimal>>> GetOrderAmountAnalytics(List<Order> Orders) {

            var GroupedOrderAmountData = Orders
                                            .GroupBy(o => o.CreatedAt)
                                            .Select(x => new ChartData<decimal> { Date = x.Key, Data = x.Sum(o => o.OrderAmount ?? 0) })
                                            .OrderBy(o => o.Date)
                                            .ToList();

            var result = GroupAndSumByDate(GroupedOrderAmountData);

            return result;

        }



        public async Task<List<ChartData<int>>> GetOrderCountAnalytics(List<Order> Orders) {

            var GroupedOrderAmountData = Orders
                                            .GroupBy(o => o.CreatedAt)
                                            .Select(x => new ChartData<int> { Date = x.Key, Data = x.Count() })
                                            .OrderBy(o => o.Date)
                                            .ToList();

            var result = GroupAndSumByDate(GroupedOrderAmountData);

            return result;

        }




        public async Task<DefaultResponse<List<ChartData<decimal>>>> GetOrderAmountAnalytics(DateTime? StartTime, DateTime? EndTime) {

            var InitialDate = StartTime ?? DateTime.MinValue;
            var LastDate = EndTime ?? DateTime.MaxValue;

            var RestaurantIDString = GetJwtValue.GetTokenFromCookie(httpContext, RestaurantIdentifier.RestaurantClaimType);

            var RestaurantID = GuidStringConverter.StringToGuid(RestaurantIDString);

            if (RestaurantID is null) {

                return new DefaultErrorResponse<List<ChartData<decimal>>>() {
                    ResponseCode = ResponseCodes.UNAUTHORIZED,
                    ResponseMessage = ResponseCodes.UNAUTHORIZED,
                    ResponseData = null
                };
            }

            List<ChartData<decimal>> OrderAmountAnalyticsData = await orderRepository.GetOrderAmountAnalytics(RestaurantID, InitialDate, LastDate);

            if (OrderAmountAnalyticsData is null) {

                return new DefaultErrorResponse<List<ChartData<decimal>>>();
            }

            var result = GroupAndSumByDate(OrderAmountAnalyticsData);

            return new DefaultSuccessResponse<List<ChartData<decimal>>>(result);

        }




        public async Task<DefaultResponse<List<ChartData<int>>>> GetOrderCountAnalytics(DateTime? StartTime, DateTime? EndTime) {

            var InitialDate = StartTime ?? DateTime.MinValue;
            var LastDate = EndTime ?? DateTime.MaxValue;

            var RestaurantIDString = GetJwtValue.GetTokenFromCookie(httpContext, RestaurantIdentifier.RestaurantClaimType);

            var RestaurantID = GuidStringConverter.StringToGuid(RestaurantIDString);

            if (RestaurantID is null) {

                return new DefaultErrorResponse<List<ChartData<int>>>() {
                    ResponseCode = ResponseCodes.UNAUTHORIZED,
                    ResponseMessage = ResponseCodes.UNAUTHORIZED,
                    ResponseData = null
                };
            }

            List<ChartData<int>> OrderCountAnalyticsData = await orderRepository.GetOrderCountAnalytics(RestaurantID, InitialDate, LastDate);

            if (OrderCountAnalyticsData is null) {

                return new DefaultErrorResponse<List<ChartData<int>>>();
            }

            var result = GroupAndSumByDate(OrderCountAnalyticsData);

            return new DefaultSuccessResponse<List<ChartData<int>>>(result);

        }


        public async Task<DefaultResponse<List<OrderItemAnalyticsData>>> GetOrderItemCountAnalytics(DateTime? StartTime, DateTime? EndTime) {

            var InitialDate = StartTime ?? DateTime.MinValue;
            var LastDate = EndTime ?? DateTime.MaxValue;

            var RestaurantIDString = GetJwtValue.GetTokenFromCookie(httpContext, RestaurantIdentifier.RestaurantClaimType);

            var RestaurantID = GuidStringConverter.StringToGuid(RestaurantIDString);

            if (RestaurantID is null) {

                return new DefaultErrorResponse<List<OrderItemAnalyticsData>>() {
                    ResponseCode = ResponseCodes.UNAUTHORIZED,
                    ResponseMessage = ResponseCodes.UNAUTHORIZED,
                    ResponseData = null
                };
            }

            List<OrderItemAnalyticsData> OrderCountAnalyticsData = await orderItemRepository.GetOrderItemCountAnalytics(RestaurantID, InitialDate, LastDate);

            if (OrderCountAnalyticsData is null) {

                return new DefaultErrorResponse<List<OrderItemAnalyticsData>>();
            }

            return new DefaultSuccessResponse<List<OrderItemAnalyticsData>>(OrderCountAnalyticsData);

        }


        private List<ChartData<T>> GroupAndSumByDate<T>(List<ChartData<T>> results) where T : struct {

            var groupedResults = results
                .GroupBy(r => r.Date.Date)  // Grouping by Date without considering the time
                .Select(g => new ChartData<T> {
                    Date = g.Key,
                    Data = (T)Convert.ChangeType(g.Sum(x => Convert.ToDecimal(x.Data)), typeof(T))  // Summing the values in Data for each grouped day
                })
                .ToList();

            return groupedResults;
        }



        private decimal CalculatePercentageChange(decimal currentMonthCount, decimal lastMonthCount) {
            if (lastMonthCount != 0) {
                return ((decimal)(currentMonthCount - lastMonthCount) / lastMonthCount) * 100;
            }
            else {
                return 0;
            }
        }
    }
}
