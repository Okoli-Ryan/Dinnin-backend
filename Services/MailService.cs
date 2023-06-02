using OrderUp_API.Interfaces;
using RabbitMQ.Client;

namespace OrderUp_API.Services {
    public class MailService : IMailService{

        private readonly IMailRepository emailRepository;

        public MailService(IMailRepository emailRepository) {
            this.emailRepository = emailRepository;
        }

        public async Task<bool> SendMail(List<string> Receipients, string Subject, string Body, string ContentType, string Sender = "Orderup@gmail.com") {

            return await emailRepository.SendMail(Receipients, Subject, Body, ContentType, "orderup@gmail.com");

        }

        public async Task<bool> SendVerificationCode(string Receipient, Guid UserID, string Code) {

            var body = $"<a href='http://localhost:5173/verify/{UserID}/{Code}'>Verify your account</a>";

            return await SendMail(new List<string> { Receipient }, "Email Verification", body, "text/html");
        }
    }
}
