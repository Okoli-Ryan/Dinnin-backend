
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using OrderUp_API.Classes.AnalyticsModels;

namespace OrderUp_API.Services
{
    public class AnalyticsService {

        private OrderRepository orderRepository;
        private HttpContext httpContext;

        public AnalyticsService(OrderRepository orderRepository, IHttpContextAccessor httpContextAccessor) {
            this.orderRepository = orderRepository;
            this.httpContext = httpContextAccessor.HttpContext;
        }

        public async Task<DefaultResponse<List<ChartData<decimal>>>> GetOrderAmountAnalytics(DateTime? StartTime, DateTime? EndTime) {

            var InitialDate = StartTime ?? DateTime.MinValue;
            var LastDate = EndTime ?? DateTime.MaxValue;

            var RestaurantIDString = GetJwtValue.GetTokenFromCookie(httpContext, RestaurantIdentifier.RestaurantClaimType);

            var RestaurantID = GuidStringConverter.StringToGuid(RestaurantIDString);

            if(RestaurantID is null) {

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

            if(RestaurantID is null) {

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

            List<OrderItemAnalyticsData> OrderCountAnalyticsData = await orderRepository.GetOrderItemCountAnalytics(RestaurantID, InitialDate, LastDate);

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
    }
}
