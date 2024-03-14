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

            foreach (var queue in MessageQueueList.getQueue()) {

                channel.QueueDeclare(queue: queue, exclusive: false);

            }

        }

        protected override Task ExecuteAsync(CancellationToken stoppingToken) {

            if (stoppingToken.IsCancellationRequested) {
                channel.Dispose();
                connection.Dispose();
                return Task.CompletedTask;
            }


            foreach (var queueName in MessageQueueList.getQueue()) {



                var consumer = new EventingBasicConsumer(channel);

                consumer.Received += async (model, ea) => {

                    var body = ea.Body.ToArray();
                    var messageString = Encoding.UTF8.GetString(body);



                    using var scope = serviceProvider.CreateScope();

                    var queueHandler = GetQueueHandler(queueName, scope);


                    await queueHandler.HandleMessageAsync(messageString);

                };

                channel.BasicConsume(queue: queueName, autoAck: true, consumer: consumer);
            }

            return Task.CompletedTask;

        }



        private static IQueueHandler GetQueueHandler(string queueName, IServiceScope scope) {

            var queueHandlers = new Dictionary<string, IQueueHandler> {
                { MessageQueueTopics.EMAIL, scope.ServiceProvider.GetRequiredService<VerificationQueueHandler<EmailMQModel>>() },
                { MessageQueueTopics.PUSH_NOTIFICATION, scope.ServiceProvider.GetRequiredService<PushNotificationQueueHandler<PushNotificationBody>>() },
                { MessageQueueTopics.FORGOT_PASSWORD, scope.ServiceProvider.GetRequiredService<ForgotPasswordQueueHandler<EmailMQModel>>() }
            };

            return queueHandlers.GetValueOrDefault(queueName);

        }
    }
}
