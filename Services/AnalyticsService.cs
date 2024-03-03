
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using OrderUp_API.Classes.AnalyticsModels;
using System.Globalization;

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





        public async Task<DefaultResponse<Order_OrderItems>> GetOrdersAndOrderItemsByDate(DateTime? StartTime, DateTime? EndTime) {

            var response = new Order_OrderItems();

            var InitialDate = StartTime ?? DateTime.MinValue;
            var LastDate = EndTime ?? DateTime.MaxValue;

            var RestaurantID = GetJwtValue.GetGuidFromCookie(httpContext, RestaurantIdentifier.RestaurantID_ClaimType);

            if (RestaurantID is null) {

                return new DefaultErrorResponse<Order_OrderItems>() {
                    ResponseCode = ResponseCodes.UNAUTHORIZED,
                    ResponseMessage = ResponseMessages.UNAUTHORIZED,
                    ResponseData = null
                };
            }

            var Orders = await orderRepository.GetOrdersByRestaurantID(RestaurantID, InitialDate, LastDate);

            if (Orders is null) return new DefaultErrorResponse<Order_OrderItems>();

            var OrderItems = Orders.SelectMany(o => o.OrderItems).ToList();

            response.Orders = Orders;
            response.OrderItems = OrderItems;

            return new DefaultSuccessResponse<Order_OrderItems>(response);
        }





        public async Task<DefaultResponse<AnalyticsData>> GetAnalyticsData(DateTime? StartTime, DateTime? EndTime) {

            var OrderAndOrderItemsResponse = await GetOrdersAndOrderItemsByDate(StartTime, EndTime);

            if (OrderAndOrderItemsResponse.ResponseCode != ResponseCodes.SUCCESS)

                return new DefaultErrorResponse<AnalyticsData> {
                    ResponseCode = OrderAndOrderItemsResponse.ResponseCode,
                    ResponseMessage = OrderAndOrderItemsResponse.ResponseMessage,
                    ResponseData = null
                };

            var Orders = OrderAndOrderItemsResponse.ResponseData.Orders;
            var OrderItems = OrderAndOrderItemsResponse.ResponseData.OrderItems;

            var response = new AnalyticsData();


            response.TotalRevenue = GetTotalRevenueData(Orders);
            response.CompletedOrders = GetCompletedOrdersData(Orders);
            response.CompletedOrderItems = GetCompletedOrderItemsData(OrderItems);

            return new DefaultSuccessResponse<AnalyticsData>(response);

        }





        public AnalyticsGrowth<int> GetCompletedOrdersData(List<Order> Orders) {

            var response = new AnalyticsGrowth<int>();

            var currentDate = DateTime.Now;

            var lastMonthDate = DateTime.Now.AddMonths(-1);

            var currentMonthOrders = Orders.Where(o => o.CreatedAt >= lastMonthDate).ToList();

            var lastMonthOrders = Orders.Where(o => o.CreatedAt >= lastMonthDate && o.CreatedAt < lastMonthDate).ToList();

            response.Total = Orders.Count;

            response.PercentageChange = CalculatePercentageChange(currentMonthOrders.Count, lastMonthOrders.Count);

            return response;

        }


        public AnalyticsGrowth<int> GetCompletedOrderItemsData(List<OrderItem> OrderItems) {

            var response = new AnalyticsGrowth<int>();

            var currentDate = DateTime.Now;

            var lastMonthDate = DateTime.Now.AddMonths(-1);

            var currentMonthOrders = OrderItems.Where(o => o.CreatedAt >= lastMonthDate).ToList();

            var lastMonthOrders = OrderItems.Where(o => o.CreatedAt >= lastMonthDate && o.CreatedAt < lastMonthDate).ToList();

            response.Total = OrderItems.Count;

            response.PercentageChange = CalculatePercentageChange(currentMonthOrders.Count, lastMonthOrders.Count);

            return response;

        }



        public AnalyticsGrowth<decimal> GetTotalRevenueData(List<Order> Orders) {

            var response = new AnalyticsGrowth<decimal>();

            var currentDate = DateTime.Now;

            var lastMonthDate = DateTime.Now.AddMonths(-1);

            var currentMonthRevenue = Orders.Where(o => o.CreatedAt >= lastMonthDate).Sum(x => x.OrderAmount ?? 0);

            var lastMonthRevenue = Orders.Where(o => o.CreatedAt >= lastMonthDate && o.CreatedAt < lastMonthDate).Sum(x => x.OrderAmount ?? 0);

            response.Total = Orders.Sum(x => x.OrderAmount ?? 0);

            response.PercentageChange = CalculatePercentageChange(currentMonthRevenue, lastMonthRevenue);

            return response;

        }





        public async Task<DefaultResponse<ChartResponse<decimal>>> GetOrderAmountAnalytics(DateTime? StartTime, DateTime? EndTime, string GroupBy = AnalyticsConstants.GROUP_BY_DATE) {

            var InitialDate = StartTime ?? DateTime.MinValue;
            var LastDate = EndTime ?? DateTime.MaxValue;

            var RestaurantID = GetJwtValue.GetGuidFromCookie(httpContext, RestaurantIdentifier.RestaurantID_ClaimType);

            if (RestaurantID is null) {

                return new DefaultErrorResponse<ChartResponse<decimal>>() {
                    ResponseCode = ResponseCodes.UNAUTHORIZED,
                    ResponseMessage = ResponseCodes.UNAUTHORIZED,
                    ResponseData = null
                };
            }

            List<ChartValue<decimal>> OrderAmountAnalyticsData = await orderRepository.GetOrderAmountAnalytics(RestaurantID, InitialDate, LastDate);

            if (OrderAmountAnalyticsData is null) {

                return new DefaultErrorResponse<ChartResponse<decimal>>();
            }

            var ChartData = GroupAndSumByDate(OrderAmountAnalyticsData, GroupBy);

            var result = new ChartResponse<decimal> {
                ChartData = ChartData,
                Key = "Revenue"
            };

            return new DefaultSuccessResponse<ChartResponse<decimal>>(result);

        }





        public async Task<DefaultResponse<ChartResponse<int>>> GetOrderCountAnalytics(DateTime? StartTime, DateTime? EndTime, string GroupBy = AnalyticsConstants.GROUP_BY_DATE) {

            var InitialDate = StartTime ?? DateTime.MinValue;
            var LastDate = EndTime ?? DateTime.MaxValue;

            var RestaurantID = GetJwtValue.GetGuidFromCookie(httpContext, RestaurantIdentifier.RestaurantID_ClaimType);

            if (RestaurantID is null) {

                return new DefaultErrorResponse<ChartResponse<int>>() {
                    ResponseCode = ResponseCodes.UNAUTHORIZED,
                    ResponseMessage = ResponseCodes.UNAUTHORIZED,
                    ResponseData = null
                };
            }

            List<ChartValue<int>> OrderCountAnalyticsData = await orderRepository.GetOrderCountAnalytics(RestaurantID, InitialDate, LastDate);

            if (OrderCountAnalyticsData is null) {

                return new DefaultErrorResponse<ChartResponse<int>>();
            }

            var ChartData = GroupAndSumByDate(OrderCountAnalyticsData, GroupBy);


            var result = new ChartResponse<int> {
                Key = "Count",
                ChartData = ChartData,
            };


            return new DefaultSuccessResponse<ChartResponse<int>>(result);

        }






        public async Task<DefaultResponse<List<OrderItemAnalyticsData>>> GetOrderItemCountAnalytics(DateTime? StartTime, DateTime? EndTime) {

            var InitialDate = StartTime ?? DateTime.MinValue;
            var LastDate = EndTime ?? DateTime.MaxValue;


            var RestaurantID = GetJwtValue.GetGuidFromCookie(httpContext, RestaurantIdentifier.RestaurantID_ClaimType);

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






        private static List<ChartValue<T>> GroupAndSumByDate<T>(List<ChartValue<T>> results, string groupBy) where T : struct {
            var groupedResults = groupBy switch {


                AnalyticsConstants.GROUP_BY_DAY => results
                            .GroupBy(r => Convert.ToDateTime(r.Date).DayOfWeek)  // Grouping by Day of the week
                            .Select(g => new ChartValue<T> {
                                Date = g.Key.ToString(),
                                Value = (T)Convert.ChangeType(g.Sum(x => Convert.ToDecimal(x.Value)), typeof(T))  // Summing the values in Data for each grouped day
                            })
                            .ToList(),


                AnalyticsConstants.GROUP_BY_MONTH => results
                            .GroupBy(r => Convert.ToDateTime(r.Date).Month)  // Grouping by Month
                            .Select(g => new ChartValue<T> {
                                Date = CultureInfo.CurrentCulture.DateTimeFormat.GetMonthName(g.Key),
                                Value = (T)Convert.ChangeType(g.Sum(x => Convert.ToDecimal(x.Value)), typeof(T))  // Summing the values in Data for each grouped month
                            })
                            .ToList(),


                AnalyticsConstants.GROUP_BY_WEEK => results
                            .GroupBy(r => CultureInfo.CurrentCulture.Calendar.GetWeekOfYear(Convert.ToDateTime(r.Date), CalendarWeekRule.FirstFourDayWeek, DayOfWeek.Monday))  // Grouping by Week of the year
                            .Select(g => new ChartValue<T> {
                                Date = "Week " + g.Key,
                                Value = (T)Convert.ChangeType(g.Sum(x => Convert.ToDecimal(x.Value)), typeof(T))  // Summing the values in Data for each grouped week
                            })
                            .ToList(),



                _ => results
                            .GroupBy(r => Convert.ToDateTime(r.Date).Date)  // Default: Grouping by Date without considering the time
                            .Select(g => new ChartValue<T> {
                                Date = g.Key.ToString(),
                                Value = (T)Convert.ChangeType(g.Sum(x => Convert.ToDecimal(x.Value)), typeof(T))  // Summing the values in Data for each grouped day
                            })
                            .ToList()


            };

            return groupedResults;
        }






        private static decimal CalculatePercentageChange(decimal currentMonthCount, decimal lastMonthCount) {
            if (lastMonthCount != 0) {
                return (currentMonthCount - lastMonthCount) / lastMonthCount * 100;
            }
            else {
                return 0;
            }
        }



    }




    public class Order_OrderItems {

        public List<Order> Orders { get; set; }

        public List<OrderItem> OrderItems { get; set; }
    }


}
