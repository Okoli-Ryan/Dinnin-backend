namespace OrderUp_API.Controllers {

    [ApiController]
    [Route("api/v1/verify")]
    public class VerificationCodeController : ControllerBase {

        readonly VerificationCodeService verificationService;

        public VerificationCodeController(VerificationCodeService verificationService) {
            this.verificationService = verificationService;
        }


        [HttpGet("{UserID}/{Code}")]
        public async Task<object> VerifyUser(Guid UserID, string Code) {

            var VerifyUserResponse = await verificationService.VerifyUser(UserID, Code);


            return VerifyUserResponse;
        }
    }
}
