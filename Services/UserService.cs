namespace OrderUp_API.Services {
    public class UserService {

        readonly IMapper mapper;
        readonly UserRepository userRepository;
        readonly IUserEntityService<User> userEntityService;
        readonly OrderUpDbContext context;
        readonly MailService mailService;
        readonly VerificationCodeService verificationCodeService;

        public UserService(IMapper mapper, UserRepository userRepository, IUserEntityService<User> userEntityService, OrderUpDbContext context, MailService mailService, VerificationCodeService verificationCodeService) {
            this.mapper = mapper;
            this.userRepository = userRepository;
            this.context = context;
            this.mailService = mailService;
            this.verificationCodeService = verificationCodeService;
            this.userEntityService = userEntityService;
        }

        public async Task<DefaultResponse<LoginResponse>> LoginAsCustomer(LoginModel loginModel) {

            User ExistingUser = await userEntityService.GetUserEntityByEmail(loginModel.Email);

            var InvalidResponse = new DefaultErrorResponse<LoginResponse>() {
                ResponseCode = ResponseCodes.INVALID_CREDENTIALS,
                ResponseMessage = ResponseMessages.INVALID_CREDENTIALS
            };


            if (ExistingUser is null) return InvalidResponse;

            var isPasswordCorrect = AuthenticationHelper.VerifyPassword(loginModel.Password, ExistingUser.Password);

            if(!isPasswordCorrect) return InvalidResponse;

            if (!ExistingUser.IsEmailConfirmed) return new DefaultErrorResponse<LoginResponse>() {
                ResponseCode = ResponseCodes.UNAUTHORIZED,
                ResponseMessage = "Confirm your email before logging in",
                ResponseData = null
            };


            var authClaims = new List<Claim>() {
                new Claim(ClaimTypes.Role, RoleTypes.Customer),
                new Claim(ClaimTypes.Email, loginModel.Email)
            };

            var token = JwtUtils.GenerateToken(authClaims);

            var response = new LoginResponse() {
                User = ParseUserResponse(ExistingUser),
                Token = token.Token,
                ExpiresAt = token.ExpiresAt
            };

            return new DefaultSuccessResponse<LoginResponse>(response);


        }

        public async Task<DefaultResponse<UserDto>> RegisterUser(User user) {

            var UniqueUser = await userEntityService.GetUserEntityByEmail(user.Email);

            if (UniqueUser is not null) return new DefaultErrorResponse<UserDto>() {
                ResponseCode = ResponseCodes.USER_ALREADY_EXIST,
                ResponseMessage = ResponseMessages.USER_ALREADY_EXIST,
                ResponseData = null
            };

            using var transaction = context.Database.BeginTransaction();

            var CreatedAccount = await Save(user);

            var verificationCode = new VerificationCode() {
                UserID = CreatedAccount.id,
                UserType = RoleTypes.Customer
            };

            var SavedVerificationCode = await verificationCodeService.CreateVerificationCode(verificationCode);

            var IsEmailSent = await mailService.SendVerificationCode(user.Email, user.ID, SavedVerificationCode.Code);

            if(!IsEmailSent) return new DefaultErrorResponse<UserDto>() {
                ResponseCode = ResponseCodes.FAILURE,
                ResponseData = null,
                ResponseMessage = "Error sending Verification Email"
            };

            transaction.Commit();

            return new DefaultSuccessResponse<UserDto>(CreatedAccount);

        }

        public async Task<UserDto> Save(User user) {

            user.Password = AuthenticationHelper.HashPassword(user.Password);

            var addedUser = await userRepository.Save(user);
            
            return ParseUserResponse(addedUser);
        }

        public async Task<List<UserDto>> Save(List<User> user) {

            var addedUser = await userRepository.Save(user);

            return ParseUserResponse(addedUser);
        }

        public async Task<UserDto> GetByID(Guid ID) {

            var user = await userRepository.GetByID(ID);

            return mapper.Map<UserDto>(user);
        }

        public async Task<UserDto> Update(User user) {

            var updatedUser = await userRepository.Update(user);

            return mapper.Map<UserDto>(updatedUser);
        }

        public async Task<bool> Delete(Guid ID) {

            return await userRepository.Delete(ID);
        }

        public async Task<bool> Delete(List<User> user) {

            return await userRepository.Delete(user);
        }

        public UserDto ParseUserResponse(User user) {

            var response = mapper.Map<UserDto>(user);
            response.password = null;
            return response;
        }

        public List<UserDto> ParseUserResponse(List<User> users) {

            var response = mapper.Map<List<UserDto>>(users);

            foreach (var user in response) { 
                user.password = null;
            }
            return response;
        }
    }
}
