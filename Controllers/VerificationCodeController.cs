namespace OrderUp_API.Controllers {

    [ApiController]
    [Route("api/v1/verify")]
    public class VerificationCodeController : ControllerBase {

        readonly VerificationCodeService verificationService;
        readonly ControllerResponseHandler ResponseHandler;
            
        public VerificationCodeController(VerificationCodeService verificationService) {
            this.verificationService = verificationService;
            ResponseHandler = new ControllerResponseHandler();
        }


        [HttpGet("{UserID}/{Code}")]
        public async Task<IActionResult> VerifyUser(Guid UserID, string Code) {

            var VerifyUserResponse = await verificationService.VerifyUser(UserID, Code);

            return ResponseHandler.HandleResponse(VerifyUserResponse);

        }
    }
}
