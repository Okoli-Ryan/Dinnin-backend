using Mailjet.Client.Resources;
using Newtonsoft.Json;

namespace OrderUp_API.MessageConsumers {
    public class VerificationQueueHandler<T> : IQueueHandler where T : EmailMQModel {

        readonly VerificationCodeService verificationCodeService;

        public VerificationQueueHandler(VerificationCodeService verificationCodeService) {
            this.verificationCodeService = verificationCodeService;
        }


        public async Task HandleMessageAsync(string message) {

            var Message = JsonConvert.DeserializeObject<T>(message);

            await verificationCodeService.SendVerificationCode(Message.ID, Message.Role, Message.Email);
        }
    }
}
