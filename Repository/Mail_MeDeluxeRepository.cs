using OrderUp_API.Classes.MailClasses;

namespace OrderUp_API.Repository {
    public class Mail_MeDeluxeRepository : RapidApiBase, IMailRepository {

        public Mail_MeDeluxeRepository() : base() {

        }

        public async Task<bool> SendMail(List<string> receipients, string subject, string body, string contentType, string sender) {

            var requestBody = new MeDeluxeRequestBody {

                Content = body,
                From = sender,
                To = receipients[0],
                Subject = subject,
            };

            var rootUrl = $"https://{ConfigurationUtil.GetConfigurationValue("Rapid_api_header:MeDeluxe-host")}/send-mail";


            var response = await NetworkService.Post<MeDeluxeRequestBody, object>(rootUrl, requestBody, null);

            return response.ResponseCode.Equals(ResponseCodes.SUCCESS);
        }
    }
}
