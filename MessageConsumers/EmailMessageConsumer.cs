using Newtonsoft.Json;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;

namespace OrderUp_API.MessageConsumers {
    public class EmailMessageConsumer : BackgroundService {

        private readonly IServiceProvider serviceProvider;
        private readonly IModel channel;
        private readonly IConnection connection;
        public EmailMessageConsumer(IServiceProvider serviceProvider) {

            this.serviceProvider = serviceProvider;

            var factory = new ConnectionFactory { HostName = "localhost" };
            connection = factory.CreateConnection();

            channel = connection.CreateModel();

            channel.QueueDeclare(queue: "Email");
        }

        protected override Task ExecuteAsync(CancellationToken stoppingToken) {
            
            if(stoppingToken.IsCancellationRequested) {
                channel.Dispose();
                connection.Dispose();
                return Task.CompletedTask;
            }

            var consumer = new EventingBasicConsumer(channel);

            consumer.Received += async (model, ea) => {

                var body = ea.Body.ToArray();
                var messageString = Encoding.UTF8.GetString(body);

                var message = JsonConvert.DeserializeObject<EmailMQModel>(messageString);


                using var scope = serviceProvider.CreateScope();

                var verificationService = scope.ServiceProvider.GetRequiredService<VerificationCodeService>();

                await verificationService.SendVerificationCode(message.ID, message.Role, message.Email);



            };

            channel.BasicConsume(queue: "Email", autoAck: true, consumer: consumer);

            return Task.CompletedTask;

        }
    }
}
