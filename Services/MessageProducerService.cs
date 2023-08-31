using Newtonsoft.Json;
using RabbitMQ.Client;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;

namespace OrderUp_API.Services {
    public class MessageProducerService : IMessageProducerService {

        private readonly IConnection connection;

        public MessageProducerService() {

            var factory = new ConnectionFactory { HostName = "host.docker.internal" };

            this.connection = factory.CreateConnection();


        }

        public void SendMessage<T>(string key, T message) {


            using var channel = connection.CreateModel();


            var json = JsonConvert.SerializeObject(message);
            var body = Encoding.UTF8.GetBytes(json);

            channel.BasicPublish(exchange: "", routingKey: key, body: body);


        }

    }
}
