using Newtonsoft.Json;
using OrderUp_API.Classes.MailClasses;
using OrderUp_API.Interfaces;
using System.Net.Http.Headers;

namespace OrderUp_API.Repository {
    public class SendGridRepository : IMailRepository {

        private readonly NetworkService networkService;
        public SendGridRepository() {

            var headers = new Dictionary<string, string>() {
                {"X-RapidAPI-Key", ConfigurationUtil.GetConfigurationValue("Rapid-api-header:key") },
                {"X-RapidAPI-Host", ConfigurationUtil.GetConfigurationValue("Rapid-api-header:host")},
            };

            this.networkService = new NetworkService(headers);

        }



        public async Task<bool> SendMail(List<string> receipients, string subject, string body, string contentType, string sender) {

            SendGridRequestBody requestBody = new SendGridRequestBody {

                Personalizations = new List<Personalization> {

                    new Personalization(receipients, subject),


                },
                From = new EmailUser { Email = sender },
                Content = new List<EmailContent> {
                    new EmailContent {
                        Type = contentType,
                        Value = body
                    }
                }

            };
            var rootUrl = $"https://{ConfigurationUtil.GetConfigurationValue("Rapid-api-header:host")}/mail/send";


            var response = await networkService.Post<SendGridRequestBody, object>(rootUrl, requestBody, null);

            return response.ResponseCode.Equals(ResponseCodes.SUCCESS);
        }
    }
}
