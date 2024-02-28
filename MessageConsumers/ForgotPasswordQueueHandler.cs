using Newtonsoft.Json;

namespace OrderUp_API.MessageConsumers {
    public class ForgotPasswordQueueHandler<T> : IQueueHandler where T : EmailMQModel {

        readonly VerificationCodeService verificationCodeService;

        public ForgotPasswordQueueHandler(VerificationCodeService verificationCodeService) {
            this.verificationCodeService = verificationCodeService;
        }


        public async Task HandleMessageAsync(string Payload) {

            var Message = JsonConvert.DeserializeObject<T>(Payload);

            await verificationCodeService.SendForgotPasswordVerificationCode(Message.ID, Message.Role, Message.Email);
        }
    }
}
