
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

        public async Task<DefaultResponse<List<OrderAmountAnalytics>>> GetOrderAmountAnalytics(DateTime? StartTime, DateTime? EndTime) {

            var InitialDate = StartTime ?? DateTime.MinValue;
            var LastDate = EndTime ?? DateTime.MaxValue;

            var RestaurantIDString = GetJwtValue.GetTokenFromCookie(httpContext, RestaurantIdentifier.RestaurantClaimType);

            var RestaurantID = GuidStringConverter.StringToGuid(RestaurantIDString);

            if(RestaurantID is null) {

                return new DefaultErrorResponse<List<OrderAmountAnalytics>>() {
                    ResponseCode = ResponseCodes.UNAUTHORIZED,
                    ResponseMessage = ResponseCodes.UNAUTHORIZED,
                    ResponseData = null
                };
            }

            List<OrderAmountAnalytics> OrderAmountAnalyticsData = await orderRepository.GetOrderAmountAnalytics(RestaurantID, InitialDate, LastDate);

            if (OrderAmountAnalyticsData is null) { 
                
                return new DefaultErrorResponse<List<OrderAmountAnalytics>>();
            }

            return new DefaultSuccessResponse<List<OrderAmountAnalytics>>(OrderAmountAnalyticsData);

        }
    }
}
