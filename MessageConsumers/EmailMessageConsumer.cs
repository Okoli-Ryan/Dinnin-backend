using Mailjet.Client.Resources;
using Newtonsoft.Json;
using OrderUp_API.Interfaces;
using OrderUp_API.Services;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System.Web.Helpers;

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



            var queueNames = new List<string> {
            {   MessageQueueTopics.EMAIL }
            };

            foreach (var queueName in queueNames) {



                var consumer = new EventingBasicConsumer(channel);

                consumer.Received += async (model, ea) => {

                    var body = ea.Body.ToArray();
                    var messageString = Encoding.UTF8.GetString(body);



                    using var scope = serviceProvider.CreateScope();

                    var queueHandler = GetQueueHandler(queueName, scope);


                    await queueHandler.HandleMessageAsync(messageString);

                    //var Message = JsonConvert.DeserializeObject<EmailMQModel>(messageString);

                    //await VerificationCodeService.SendVerificationCode(Message.ID, Message.Role, Message.Email);

                };

                channel.BasicConsume(queue: MessageQueueTopics.EMAIL, autoAck: true, consumer: consumer);
            }

            return Task.CompletedTask;

        }

        private static IQueueHandler GetQueueHandler(string queueName, IServiceScope scope) {

            var queueHandlers = new Dictionary<string, IQueueHandler> {
                { MessageQueueTopics.EMAIL, scope.ServiceProvider.GetRequiredService<VerificationQueueHandler<EmailMQModel>>() }
            };

            return queueHandlers.GetValueOrDefault(queueName);

        }
    }
}
