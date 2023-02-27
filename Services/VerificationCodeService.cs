namespace OrderUp_API.Services {
    public class VerificationCodeService {

        private readonly VerificationCodeRepository verificationCodeRepository;
        private readonly AdminRepository adminRepository;
        private readonly UserRepository userRepository;
        private readonly IUserEntityService<Admin> AdminUserEntityService;
        private readonly IUserEntityService<User> CustomerUserEntityService;

        public VerificationCodeService(VerificationCodeRepository verificationCodeRepository, IUserEntityService<User> CustomerUserEntityService, IUserEntityService<Admin> AdminUserEntityService, AdminRepository adminRepository, UserRepository userRepository) {
            this.verificationCodeRepository = verificationCodeRepository;
            this.userRepository = userRepository;
            this.adminRepository = adminRepository;
            this.CustomerUserEntityService = CustomerUserEntityService;
            this.AdminUserEntityService = AdminUserEntityService;
       
        }

        public async Task<VerificationCode> CreateVerificationCode(VerificationCode VerificationModel) {

            VerificationModel.ExpiryDate = DateTime.Now.AddHours(1);
            VerificationModel.Code = RandomStringGenerator.GenerateRandomString(10);

            var PendingVerificationModel = await verificationCodeRepository.GetPendingVerificationCode(VerificationModel.UserID);

            if (PendingVerificationModel is not null) {

                PendingVerificationModel.ActiveStatus = false;

                await verificationCodeRepository.Update(PendingVerificationModel);
            }

            var SavedVerificationCode = await verificationCodeRepository.Save(VerificationModel);

            return SavedVerificationCode;

        }

        public async Task<VerificationCode> VerifyVerificationCode(Guid UserId, string Code) {

            var VerificationModel = await verificationCodeRepository.GetActiveVerificationCodeByCode(UserId, Code);

            if (VerificationModel is not null && DateTime.Now < VerificationModel.ExpiryDate) {

                VerificationModel.ActiveStatus = false;

                await verificationCodeRepository.Update(VerificationModel);

            }

            return VerificationModel;

        }

        public async Task<object> VerifyUser(Guid UserId, string Code) {

            var VerificationModel = await VerifyVerificationCode(UserId, Code);

            if (VerificationModel is null) {

                return new DefaultErrorResponse<IUserEntityDto>() {

                    ResponseCode = ResponseCodes.INVALID_TOKEN,
                    ResponseMessage = "Verification code doesn't exist",
                    ResponseData = null
                };

            }

            if (VerificationModel.UserType.Equals(RoleTypes.Admin)) {

                var VerifyAdminResponse = await AdminUserEntityService.VerifyUserEntity<AdminDto, AdminRepository>(VerificationModel.UserID, adminRepository);

                return VerifyAdminResponse;

            }

            if (VerificationModel.UserType.Equals(RoleTypes.Customer)) {

                var VerifyUserResponse = await CustomerUserEntityService.VerifyUserEntity<UserDto, UserRepository>(VerificationModel.UserID, userRepository);

                return VerifyUserResponse;

            }

            return new DefaultErrorResponse<IUserEntityDto>() {
                ResponseCode = ResponseCodes.NOT_FOUND,
                ResponseMessage = ResponseMessages.NOT_FOUND,
                ResponseData = null
            };

        }

    }
}
