using OrderUp_API.Interfaces;
using RabbitMQ.Client;

namespace OrderUp_API.Services {
    public class MailService : IMailService{

        private readonly IMailRepository emailRepository;

        public MailService(IMailRepository emailRepository) {
            this.emailRepository = emailRepository;
        }

        public async Task<bool> SendMail(List<string> Receipients, string Subject, string Body, string ContentType, string Sender = "Dinnin@firebese.com") {

            return await emailRepository.SendMail(Receipients, Subject, Body, ContentType, Sender);

        }

        public async Task<bool> SendVerificationCode(string Receipient, Guid UserID, string Code) {

            var dashboardClient = ConfigurationUtil.GetConfigurationValue("Dinnin-Dashboard-Client");

            var body = $"<a href='{dashboardClient}/verify/{UserID}/{Code}'>Verify your account</a>";

            return await SendMail(new List<string> { Receipient }, "Email Verification", body, "text/html");
        }
    }
}
