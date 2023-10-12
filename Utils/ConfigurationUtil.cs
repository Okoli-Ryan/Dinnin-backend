namespace OrderUp_API.Utils
{
    public static class ConfigurationUtil
    {

        //public static string GetConfigurationValue(string key) {


        //    var builder = new ConfigurationBuilder()
        //                    .SetBasePath(Directory.GetCurrentDirectory())
        //                    .AddJsonFile("appsettings.json");

        //    configuration = builder.Build();

        //    return configuration.GetValue<string>(key);
        //}

        public static string GetConfigurationValue(string configKey)
        {
            // Check if the configKey corresponds to an environment variable
            string envValue = Environment.GetEnvironmentVariable(configKey);
            if (!string.IsNullOrEmpty(envValue))
            {
                return envValue;
            }

            // Check if the configKey corresponds to a value in secrets.json
            string secretsJsonValue = GetSecretsJsonValue(configKey);
            if (!string.IsNullOrEmpty(secretsJsonValue))
            {
                return secretsJsonValue;
            }

            return string.Empty;
        }

        private static string GetSecretsJsonValue(string configKey)
        {
            // Specify the absolute path to the directory containing secrets.json
            string secretsDirectoryPath = @"C:\Users\okoli\AppData\Roaming\Microsoft\UserSecrets\f85c94e3-d73f-41d8-8cfd-201e9923d858";

            // Load secrets.json and check if the key exists
            var secretsConfiguration = new ConfigurationBuilder().AddUserSecrets<Program>().Build();


            return secretsConfiguration[configKey];
        }

    }
}
