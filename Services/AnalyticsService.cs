using OrderUp_API.Classes.AnalyticsModels;

namespace OrderUp_API.Services
{
    public class AnalyticsService {

        private OrderRepository orderRepository;

        public AnalyticsService(OrderRepository orderRepository) {
            this.orderRepository = orderRepository;
        }

        public async Task<DefaultResponse<T>> GetOrderAmountAnalytics<T>(DateTime? StartTime, DateTime? EndTime) where T : List<OrderAmountAnalytics> {

            var InitialDate = StartTime ?? DateTime.MinValue;
            var LastDate = EndTime ?? DateTime.MinValue;

            var OrderAmountAnalyticsData = await orderRepository.GetOrderAmountAnalytics(InitialDate, LastDate);

            if (OrderAmountAnalyticsData is null) { 
                
                return new DefaultErrorResponse<T>();
            }

            return new DefaultSuccessResponse<T>((T) OrderAmountAnalyticsData);

        }
    }
}
