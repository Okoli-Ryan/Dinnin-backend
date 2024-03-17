namespace OrderUp_API.Services {
    public class MailService : IMailService {

        private readonly IMailRepository emailRepository;

        public MailService(IMailRepository emailRepository) {
            this.emailRepository = emailRepository;
        }

        public async Task<bool> SendMail(List<string> Receipients, string Subject, string Body, string ContentType, string Sender = "Dinnin@firebese.com") {

            return await emailRepository.SendMail(Receipients, Subject, Body, ContentType, Sender);

        }

        public async Task<bool> SendVerificationCode(string Receipient, string Code) {

            var dashboardClient = ConfigurationUtil.GetConfigurationValue("Dinnin_Dashboard_Client");

            var body = $"<a href='{dashboardClient}/verify/{Code}'>Verify your account</a>";

            return await SendMail(new List<string> { Receipient }, "Email Verification", body, "text/html");
        }



        public async Task<bool> SendForgotPasswordEmail(string Receipient, string Code) {

            var dashboardClient = ConfigurationUtil.GetConfigurationValue("Dinnin_Dashboard_Client");

            var body = $"<a href='{dashboardClient}/reset/{Code}'>Click link to reset your password</a>";

            return await SendMail(new List<string> { Receipient }, "Forgot Password", body, "text/html");
        }

        public async Task<bool> SendNewStaffVerificationEmail(Admin newAdmin, string RestaurantName, string Code) {

            var dashboardClient = ConfigurationUtil.GetConfigurationValue("Dinnin_Dashboard_Client");

            var body = $"You have been invited to {RestaurantName} for the role of: '{newAdmin.Position}.'" +
                $"Your Login Email is {newAdmin.Email}" +
                $"<a href='{dashboardClient}/register-staff/{Code}'>Click link to set your password</a>";

            return await SendMail(new List<string> { newAdmin.RecoveryEmail }, "Staff Registration", body, "text/html");
        }
    }
}
