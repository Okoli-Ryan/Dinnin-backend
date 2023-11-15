namespace OrderUp_API.Classes {
    public class RapidApiBase {

        public NetworkService NetworkService { get; set; }

        public RapidApiBase() {

            var headers = new Dictionary<string, string>() {
                {"X-RapidAPI-Key", ConfigurationUtil.GetConfigurationValue("Rapid-api-header:key") },
                {"X-RapidAPI-Host", ConfigurationUtil.GetConfigurationValue("Rapid-api-header:host")},
            };

            NetworkService = new NetworkService(headers);

        }
    }
}
