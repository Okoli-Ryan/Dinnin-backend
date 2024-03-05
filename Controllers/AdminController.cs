﻿namespace OrderUp_API.Controllers {

    [ApiController]
    [ServiceFilter(typeof(ModelValidationActionFilter))]
    [Route("api/v1/admin")]
    public class AdminController : ControllerBase {

        readonly AdminService adminService;
        readonly IMapper mapper;
        readonly ControllerResponseHandler ResponseHandler;

        public AdminController(AdminService adminService, IMapper mapper) {

            this.adminService = adminService;
            this.mapper = mapper;
            ResponseHandler = new ControllerResponseHandler();
        }

        [HttpGet("test")]
        public IActionResult Test()
        {
            return Ok("This works");
        }


        [HttpGet("{ID}")]
        public async Task<IActionResult> GetAdminByID(Guid ID) {

            var addedAdmin = await adminService.GetByID(ID);

            if (addedAdmin is null) return Ok(new DefaultErrorResponse<AdminDto>());

            return Ok(new DefaultSuccessResponse<AdminDto>(addedAdmin));
            //return ResponseHandler.HandleResponse<AdminDto(addedAdmin);
        }



        [HttpPost()]
        //[Authorize(Roles = RoleTypes.SuperAdmin)]
        public async Task<IActionResult> AddAdmin([FromBody] AdminDto adminDto) {

            var mappedAdmin = mapper.Map<Admin>(adminDto);

            var addedAdmin = await adminService.RegisterAdmin(mappedAdmin);

            if (!addedAdmin.ResponseCode.Equals(ResponseCodes.SUCCESS)) {
                return BadRequest(addedAdmin);
            }

            return Ok(addedAdmin.ResponseData);
        }


        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginModel loginModel) {

            var response = await adminService.LoginAsAdmin(loginModel);

            return ResponseHandler.HandleResponse(response);
        }
        
        
        [HttpPost("forgot-password")]
        public async Task<IActionResult> ForgotPassword([FromBody] string Email) {

            var response = await adminService.HandleForgotPasswordRequest(Email);

            return ResponseHandler.HandleResponse(response);
        }


        [HttpPost("reset-password/{UserID}")]
        public async Task<IActionResult> ResetPassword(Guid UserID, [FromBody] string NewPassword) {

            var response = await adminService.HandleResetPassword(UserID, NewPassword);

            return ResponseHandler.HandleResponse(response);
        }



        [HttpPut()]
        public async Task<IActionResult> UpdateAdmin([FromBody] AdminDto adminDto) {

            var response = await adminService.Update(adminDto);

            return ResponseHandler.HandleResponse(response);

        }



        [HttpDelete("{ID}")]
        public async Task<IActionResult> DeleteAdmin(Guid ID) {

            var isDeletedAdmin = await adminService.Delete(ID);

            if (!isDeletedAdmin) return Ok(new DefaultErrorResponse<bool>());

            return Ok(new DefaultSuccessResponse<bool>(isDeletedAdmin));
        }





    }
}
