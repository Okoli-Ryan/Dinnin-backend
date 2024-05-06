using Microsoft.AspNetCore.Authorization;
using OrderUp_API.Attributes;

namespace OrderUp_API.Controllers {

    [ApiController]
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
        [Authorize(Roles = RoleTypes.SuperAdmin)]
        public IActionResult Test() {
            return Ok("This works");
        }


        [HttpGet("{ID:guid}")]
        public async Task<IActionResult> GetAdminByID(Guid ID) {

            var addedAdmin = await adminService.GetByID(ID);

            if (addedAdmin is null) return Ok(new DefaultErrorResponse<AdminDto>());

            return Ok(new DefaultSuccessResponse<AdminDto>(addedAdmin));
            //return ResponseHandler.HandleResponse<AdminDto(addedAdmin);
        }

        [PermissionRequired(PermissionName.STAFF__VIEW_STAFF)]
        [HttpGet]
        public async Task<IActionResult> GetListOfAdmins(string name, string email, string phonenumber, int page, int size, DateTime? startDate, DateTime? endDate) {

            var response = await adminService.GetAdminList(name, email, phonenumber, page, size, startDate, endDate);

            return ResponseHandler.HandleResponse(response);
        }



        [HttpPost()]
        public async Task<IActionResult> AddAdmin([FromBody] AdminDto adminDto) {

            var mappedAdmin = mapper.Map<Admin>(adminDto);

            var addedAdmin = await adminService.RegisterAdmin(mappedAdmin);

            if (!addedAdmin.ResponseCode.Equals(ResponseCodes.SUCCESS)) {
                return BadRequest(addedAdmin);
            }

            return Ok(addedAdmin.ResponseData);
        }


        [HttpPost("add-staff")]
        [PermissionRequired(PermissionName.STAFF__CREATE_STAFF)]
        public async Task<IActionResult> RegisterStaff([FromBody] AdminDto adminDto) {

            var addedAdmin = await adminService.RegisterStaff(adminDto);

            return ResponseHandler.HandleResponse(addedAdmin);

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


        [HttpPost("reset-password")]
        public async Task<IActionResult> ResetPassword([FromBody] ResetPasswordPayload PasswordPayload) {

            var response = await adminService.HandleResetPassword(PasswordPayload.Code, PasswordPayload.NewPassword);

            return ResponseHandler.HandleResponse(response);
        }



        [HttpPut()]
        public async Task<IActionResult> UpdateAdmin([FromBody] AdminDto adminDto) {

            var response = await adminService.Update(adminDto);

            return ResponseHandler.HandleResponse(response);

        }

        [PermissionRequired(PermissionName.PERMISSIONS__VIEW_PERMISSIONS)]
        [HttpGet("permissions/{ID}")]
        public async Task<IActionResult> GetAdminPermissions(Guid ID) { 
        
            var response = await adminService.GetAdminPermissions(ID);

            return ResponseHandler.HandleResponse(response);
        }



        [PermissionRequired(PermissionName.PERMISSIONS__UPDATE_PERMISSIONS)]
        [HttpPost("permissions/{ID}")]
        public async Task<IActionResult> UpdateAdminPermissions(Guid ID, [FromBody] List<int> PermissionIds) { 
            var response = await adminService.UpdateAdminPermissions(ID, PermissionIds);
        
            return ResponseHandler.HandleResponse(response);
        }



        //[HttpDelete("{ID}")]
        //public async Task<IActionResult> DeleteAdmin(Guid ID) {

        //    var isDeletedAdmin = await adminService.Delete(ID);

        //    if (!isDeletedAdmin) return Ok(new DefaultErrorResponse<bool>());

        //    return Ok(new DefaultSuccessResponse<bool>(isDeletedAdmin));
        //}

        [HttpGet("logout")]
        public async Task<IActionResult> Logout() {
            await adminService.Logout();
            return Ok("/access-denied");
        }


        [HttpGet("/access-denied")]
        public IActionResult AccessDenied() {

            return ResponseHandler.HandleResponse(new DefaultErrorResponse<object>() {
                ResponseCode = ResponseCodes.UNAUTHORIZED,
                ResponseData = null,
                ResponseMessage = ResponseMessages.UNAUTHORIZED
            }); ;
        }

    }

    public class ResetPasswordPayload {

        public string NewPassword { get; set; }

        public string Code { get; set; }
    }
}
