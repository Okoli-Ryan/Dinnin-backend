using OrderUp_API.Interfaces;

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

            var body = $"Use this code to verify your account: {Code}, user id: {UserID}";

            return await SendMail(new List<string> { Receipient }, "Email Verification", body, "text/plain");
        }
    }
}
