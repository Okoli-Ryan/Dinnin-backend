
namespace OrderUp_API.Controllers {

    [ApiController]
    [ServiceFilter(typeof(ModelValidationActionFilter))]
    [Route("api/v1/user")]
    public class UserController : ControllerBase {

        readonly UserService userService;
        readonly IMailService emailService;
        readonly IMapper mapper;

        public UserController(UserService userService, IMapper mapper, IMailService emailService) {

            this.userService = userService;
            this.mapper = mapper;
            this.emailService = emailService;
        }

        [HttpGet("{ID}")]
        public async Task<IActionResult> GetUserByID(Guid ID) {

            var addedUser = await userService.GetByID(ID);

            if (addedUser is null) return Ok(new DefaultErrorResponse<User>());

            return Ok(new DefaultSuccessResponse<UserDto>(addedUser));
        }



        [HttpPost()]
        public async Task<IActionResult> AddUser([FromBody] UserDto userDto) {

            var mappedUser = mapper.Map<User>(userDto);

            var addedUser = await userService.RegisterUser(mappedUser);

            return Ok(addedUser);
        }


        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginModel loginModel) {

            var response = await userService.LoginAsCustomer(loginModel);

            return Ok(response);
        }



        [HttpPut()]
        public async Task<IActionResult> UpdateUser([FromBody] UserDto userDto) {


            var mappedUser = mapper.Map<User>(userDto);

            var updatedUser = await userService.Update(mappedUser);

            if (updatedUser is null) return Ok(new DefaultErrorResponse<UserDto>());


            return Ok(new DefaultSuccessResponse<UserDto>(updatedUser));

        }



        [HttpDelete("{ID}")]
        public async Task<IActionResult> DeleteUser(Guid ID) {

            var isDeletedUser = await userService.Delete(ID);

            if (!isDeletedUser) return Ok(new DefaultErrorResponse<bool>());

            return Ok(new DefaultSuccessResponse<bool>(isDeletedUser));
        }





    }
}
