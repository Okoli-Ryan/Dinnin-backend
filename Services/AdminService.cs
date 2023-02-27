namespace OrderUp_API.Services {
    public class AdminService {

        private readonly AdminRepository adminRepository;
        private readonly IUserEntityService<Admin> userEntityService;
        private readonly IMapper mapper;
        private readonly IMailService mailService;
        private readonly OrderUpDbContext context;
        private readonly VerificationCodeService verificationCodeService;

        public AdminService(AdminRepository adminRepository, IMapper mapper, IUserEntityService<Admin> userEntityService, IMailService mailService, OrderUpDbContext context, VerificationCodeService verificationCodeService) {
            this.adminRepository = adminRepository;
            this.mapper = mapper;
            this.context = context;
            this.mailService = mailService;
            this.verificationCodeService = verificationCodeService;
            this.userEntityService = userEntityService;
        }

        public async Task<DefaultResponse<AdminDto>> RegisterAdmin(Admin Admin) {

            var ExistingAdmin = await userEntityService.GetUserEntityByEmail(Admin.Email);

            if (ExistingAdmin is not null) return new DefaultResponse<AdminDto>() {
                ResponseCode = ResponseCodes.USER_ALREADY_EXIST,
                ResponseMessage = ResponseMessages.USER_ALREADY_EXIST,
                ResponseData = null
            };


            Admin.Role = RoleTypes.Admin;
            Admin.IsEmailConfirmed = false;

            using var transaction = context.Database.BeginTransaction();

            var CreatedAccount = await Save(Admin);

            var verificationCode = new VerificationCode() {
                UserID = CreatedAccount.id,
                UserType = RoleTypes.Admin
            };

            var createdVerificationCode = await verificationCodeService.CreateVerificationCode(verificationCode);

            var IsSentVerificationEmail = await mailService.SendVerificationCode(Admin.Email, Admin.ID, createdVerificationCode.Code);

            if(!IsSentVerificationEmail) return new DefaultErrorResponse<AdminDto>();

            transaction.Commit();

            return new DefaultSuccessResponse<AdminDto>(CreatedAccount);

        }


        //public async Task<DefaultResponse<AdminDto>> ConfirmAdminAccount(Guid UserID, string Code) {

        //    var VerifyAdminResponse 

        //}

        public async Task<DefaultResponse<AdminLoginResponse>> LoginAsAdmin(LoginModel loginModel) {

            var ExistingAdmin = await userEntityService.GetUserEntityByEmail(loginModel.Email);

            var InvalidResponse = new DefaultErrorResponse<AdminLoginResponse>() {
                ResponseCode = ResponseCodes.INVALID_CREDENTIALS,
                ResponseMessage = ResponseMessages.INVALID_CREDENTIALS
            };


            if (ExistingAdmin is null) return InvalidResponse;

            var isPasswordCorrect = AuthenticationHelper.VerifyPassword(loginModel.Password, ExistingAdmin.Password);

            if (!isPasswordCorrect) return InvalidResponse;

            if (!ExistingAdmin.IsEmailConfirmed) return new DefaultErrorResponse<AdminLoginResponse>() {
                ResponseCode = ResponseCodes.UNAUTHORIZED,
                ResponseMessage = "Confirm your email before logging in",
                ResponseData = null
            };

            var authClaims = new List<Claim>() {    
                new Claim(ClaimTypes.Role, ExistingAdmin.Role),
                new Claim(ClaimTypes.Email, ExistingAdmin.Email),
                new Claim(RestaurantIdentifier.RestaurantClaimType, ExistingAdmin.RestaurantID.ToString())
            };

            var token = JwtUtils.GenerateToken(authClaims);

            var response = new AdminLoginResponse() {
                Admin = ParseAdminResponse(ExistingAdmin),
                Token = token.Token,
                ExpiresAt = token.ExpiresAt
            };

            return new DefaultSuccessResponse<AdminLoginResponse>(response);


        }

        public async Task<AdminDto> Save(Admin admin) {

            admin.Password = AuthenticationHelper.HashPassword(admin.Password);

            var addedAdmin = await adminRepository.Save(admin);

            return ParseAdminResponse(addedAdmin);
        }

        public async Task<List<AdminDto>> Save(List<Admin> admin) {

            var addedAdmin = await adminRepository.Save(admin);

            return ParseAdminResponse(addedAdmin);
        }

        public async Task<AdminDto> GetByID(Guid ID) {

            var admin = await adminRepository.GetByID(ID);

            return mapper.Map<AdminDto>(admin);
        }

        public async Task<AdminDto> Update(Admin admin) {

            var updatedAdmin = await adminRepository.Update(admin);

            return mapper.Map<AdminDto>(updatedAdmin);
        }

        public async Task<bool> Delete(Guid ID) {

            return await adminRepository.Delete(ID);
        }

        public async Task<bool> Delete(List<Admin> admin) {

            return await adminRepository.Delete(admin);
        }

        public AdminDto ParseAdminResponse(Admin admin) {

            var response = mapper.Map<AdminDto>(admin);
            response.password = null;
            return response;
        }

        public List<AdminDto> ParseAdminResponse(List<Admin> admins) {

            var response = mapper.Map<List<AdminDto>>(admins);

            foreach (var admin in response) {
                admin.password = null;
            }
            return response;
        }
    }
}
