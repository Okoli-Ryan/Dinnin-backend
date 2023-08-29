
using Microsoft.AspNetCore.Authorization;

namespace OrderUp_API.Controllers {

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

        [AllowAnonymous]
        [HttpGet("test")]
        public IActionResult Test()
        {
            var result = new List<int>() { 1, 2, 3 };
            return Ok(result);
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



        [HttpPatch()]
        public async Task<IActionResult> UpdateAdmin([FromBody] AdminDto adminDto) {


            var mappedAdmin = mapper.Map<Admin>(adminDto);

            var updatedAdmin = await adminService.Update(mappedAdmin);

            if (updatedAdmin is null) return Ok(new DefaultErrorResponse<AdminDto>());


            return Ok(new DefaultSuccessResponse<AdminDto>(updatedAdmin));

        }



        [HttpDelete("{ID}")]
        public async Task<IActionResult> DeleteAdmin(Guid ID) {

            var isDeletedAdmin = await adminService.Delete(ID);

            if (!isDeletedAdmin) return Ok(new DefaultErrorResponse<bool>());

            return Ok(new DefaultSuccessResponse<bool>(isDeletedAdmin));
        }





    }
}
