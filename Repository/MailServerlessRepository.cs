using OrderUp_API.Classes.MailClasses;

namespace OrderUp_API.Repository {
    public class MailServerlessRepository : RapidApiBase, IMailRepository {

        public MailServerlessRepository() : base() { }


        public async Task<bool> SendMail(List<string> receipients, string subject, string body, string contentType, string sender) {

            EmailRequestBody requestBody = new() {

                Personalizations = new List<Personalization> {

                    new Personalization(receipients),


                },
                From = new EmailUser { Email = sender },
                Content = new List<EmailContent> {
                    new EmailContent {
                        Type = contentType,
                        Value = body
                    }
                },
                Subject = subject,

            };
            var rootUrl = $"https://{ConfigurationUtil.GetConfigurationValue("Rapid_api_header_host")}/send";


            var response = await NetworkService.Post<EmailRequestBody, object>(rootUrl, requestBody, null);

            return response.ResponseCode.Equals(ResponseCodes.SUCCESS);
        }
    }
}
