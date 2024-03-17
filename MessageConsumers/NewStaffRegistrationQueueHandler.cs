using Newtonsoft.Json;

namespace OrderUp_API.MessageConsumers {
    public class NewStaffRegistrationQueueHandler<T> : IQueueHandler where T : StaffRegistrationModel {

        readonly VerificationCodeService verificationCodeService;

        public NewStaffRegistrationQueueHandler(VerificationCodeService verificationCodeService) {
            this.verificationCodeService = verificationCodeService;
        }


        public async Task HandleMessageAsync(string Payload) {

            var Message = JsonConvert.DeserializeObject<T>(Payload);

            await verificationCodeService.SendNewStaffVerificationEmail(Message.Admin, Message.RestaurantName);
        }
    }
}
