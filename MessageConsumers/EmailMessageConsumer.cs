using Newtonsoft.Json;
using OrderUp_API.Interfaces;
using OrderUp_API.Services;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;

namespace OrderUp_API.MessageConsumers {
    public class EmailMessageConsumer : BackgroundService {

        private readonly IServiceProvider serviceProvider;
        private readonly IModel channel;
        private readonly IConnection connection;
        public EmailMessageConsumer(IServiceProvider serviceProvider) {

            this.serviceProvider = serviceProvider;

            var ConnectionString = ConfigurationUtil.GetConfigurationValue("RabbitMQ_URI");

            Uri ConnectionUri = new(ConnectionString);

            var factory = new ConnectionFactory { Uri = ConnectionUri };

            connection = factory.CreateConnection();

            channel = connection.CreateModel();

            channel.QueueDeclare(queue: MessageQueueTopics.EMAIL);
        }

        protected override Task ExecuteAsync(CancellationToken stoppingToken) {

            if (stoppingToken.IsCancellationRequested) {
                channel.Dispose();
                connection.Dispose();
                return Task.CompletedTask;
            }

            using var scope = serviceProvider.CreateScope();
            var verificationService = scope.ServiceProvider.GetRequiredService<VerificationCodeService>();



            var queueHandlers = new Dictionary<string, IQueueHandler> {
        {   MessageQueueTopics.EMAIL,
            new VerificationQueueHandler<EmailMQModel>(verificationService)
        }
            };

            foreach (var queueHandlerEntry in queueHandlers) {

                var queueName = queueHandlerEntry.Key;
                var queueHandler = queueHandlerEntry.Value;


                var consumer = new EventingBasicConsumer(channel);

                consumer.Received += async (model, ea) => {

                    var body = ea.Body.ToArray();
                    var messageString = Encoding.UTF8.GetString(body);

                    await queueHandler.HandleMessageAsync(messageString);

                };

                channel.BasicConsume(queue: queueName, autoAck: true, consumer: consumer);
            }

            return Task.CompletedTask;

        }
    }
}
