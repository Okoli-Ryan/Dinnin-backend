namespace OrderUp_API.Classes {
    public class RapidApiBase {

        public NetworkService NetworkService { get; set; }

        public RapidApiBase() {

            var headers = new Dictionary<string, string>() {
                {"X-RapidAPI-Key", ConfigurationUtil.GetConfigurationValue("Rapid_api_header_key") },
                {"X-RapidAPI-Host", ConfigurationUtil.GetConfigurationValue("Rapid_api_header_host")},
            };

            NetworkService = new NetworkService(headers);

        }
    }
}
