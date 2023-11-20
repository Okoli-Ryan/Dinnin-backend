namespace OrderUp_API.Classes.AnalyticsModels
{
    public class AnalyticsData{

        public AnalyticsGrowth<int> CompletedOrders { get; set; }

        public AnalyticsGrowth<int> CompletedOrderItems { get; set; }

        public AnalyticsGrowth<decimal> TotalRevenue { get; set; }

        public List<ChartData<decimal>> OrderAmountChartData { get; set; }

        public List<ChartData<int>> OrderCountChartData { get; set; }

    }

    public class AnalyticsGrowth<T> {

        public T Total { get; set; }

        public decimal PercentageChange { get; set; }
    }
}
