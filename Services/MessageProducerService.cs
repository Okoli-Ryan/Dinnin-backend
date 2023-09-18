using Newtonsoft.Json;
using RabbitMQ.Client;

namespace OrderUp_API.Services {
    public class MessageProducerService : IMessageProducerService {

        private readonly IConnection connection;

        public MessageProducerService() {


            var ConnectionString = ConfigurationUtil.GetConfigurationValue("RabbitMQ_URI");

            Uri ConnectionUri = new(ConnectionString);

            var factory = new ConnectionFactory { Uri = ConnectionUri };

            connection = factory.CreateConnection();


        }

        public void SendMessage<T>(string key, T message) {


            using var channel = connection.CreateModel();


            var json = JsonConvert.SerializeObject(message);
            var body = Encoding.UTF8.GetBytes(json);

            channel.BasicPublish(exchange: "", routingKey: key, body: body);


        }

        public void Dispose() {
            connection.Dispose();
        }

    }
}
