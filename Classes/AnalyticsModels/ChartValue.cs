namespace OrderUp_API.Classes.AnalyticsModels {
    public class ChartValue<T> {

        public T Value { get; set; }

        public string Date { get; set; }

    }

    public class ChartResponse<T> {

        public string Key { get; set; }

        public List<ChartValue<T>> ChartData { get; set; } = new List<ChartValue<T>>();
    }
}
