namespace OrderUp_API.Utils {
    public class ConfigurationUtil {

        private static IConfiguration configuration;
        public static string GetConfigurationValue(string key) {


            var builder = new ConfigurationBuilder()
                            .SetBasePath(Directory.GetCurrentDirectory())
                            .AddJsonFile("appsettings.json");

            configuration = builder.Build();

            return configuration.GetValue<string>(key);
        }
    }
}
