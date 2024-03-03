using OrderUp_API.Classes;
using OrderUp_API.DTOs;

namespace OrderUp_API.Services {
    public class VerificationCodeService {

        private readonly VerificationCodeRepository verificationCodeRepository;
        private readonly AdminRepository adminRepository;
        private readonly UserRepository userRepository;
        private readonly IMapper mapper;
        private readonly IUserEntityService<Admin> AdminUserEntityService;
        private readonly IUserEntityService<User> CustomerUserEntityService;
        private readonly MailService mailService;

        public VerificationCodeService(VerificationCodeRepository verificationCodeRepository, IUserEntityService<User> CustomerUserEntityService, IUserEntityService<Admin> AdminUserEntityService, AdminRepository adminRepository, UserRepository userRepository, MailService mailService, IMapper mapper) {
            this.verificationCodeRepository = verificationCodeRepository;
            this.userRepository = userRepository;
            this.adminRepository = adminRepository;
            this.CustomerUserEntityService = CustomerUserEntityService;
            this.AdminUserEntityService = AdminUserEntityService;
            this.mailService = mailService;
            this.mapper = mapper;
        }

        /// <summary>
        /// Creates a verification code based on UserID, RoleType, and email, then saves to the Database and sends the email
        /// </summary>
        public async Task<bool> SendCreateAccountVerificationCode(Guid UserID, string RoleType, string UserEmail) {

            var verificationCode = new VerificationCode() {
                UserID = UserID,
                UserType = RoleType
            };

            var createdVerificationCode = await CreateVerificationCode(UserID, RoleType);

            if (createdVerificationCode == null) return false;

            return await mailService.SendVerificationCode(UserEmail, UserID, createdVerificationCode.Code);
        }


        public async Task<bool> SendForgotPasswordVerificationCode(Guid UserID, string RoleType, string UserEmail) {

            var createdVerificationCode = await CreateVerificationCode(UserID, RoleType);

            if (createdVerificationCode == null) return false;

            return await mailService.SendForgotPasswordEmail(UserEmail, UserID, createdVerificationCode.Code);
        }


        public async Task<VerificationCode> CreateVerificationCode(Guid UserID, string RoleType) {

            var verificationModel = new VerificationCode() {
                UserID = UserID,
                UserType = RoleType
            };

            verificationModel.ExpiryDate = DateTime.Now.AddHours(1);
            verificationModel.Code = RandomStringGenerator.GenerateRandomString(10);

            var PendingVerificationModel = await verificationCodeRepository.GetPendingVerificationCode(verificationModel.UserID);

            if (PendingVerificationModel is not null) {

                PendingVerificationModel.ActiveStatus = false;

                await verificationCodeRepository.Update(PendingVerificationModel);
            }

            var SavedVerificationCode = await verificationCodeRepository.Save(verificationModel);

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

        public async Task<DefaultResponse<IUserEntityDto>> VerifyUser(Guid UserId, string Code) {

            var VerificationModel = await VerifyVerificationCode(UserId, Code);

            var InvalidToken = new DefaultErrorResponse<IUserEntityDto>() {

                ResponseCode = ResponseCodes.INVALID_TOKEN,
                ResponseMessage = "Verification code doesn't exist",
                ResponseData = null
            };

            if (VerificationModel is null) {

                return InvalidToken;

            }

            if (VerificationModel.UserType.Equals(RoleTypes.Admin)) {

                var VerifyAdminResponse = await AdminUserEntityService.VerifyUserEntity(VerificationModel.UserID, adminRepository);

                if (VerifyAdminResponse is null) {
                    return InvalidToken;
                }

                var AdminDtoResponse = mapper.Map<AdminDto>(VerifyAdminResponse);

                return new DefaultSuccessResponse<IUserEntityDto>(AdminDtoResponse);

            }

            if (VerificationModel.UserType.Equals(RoleTypes.Customer)) {

                var VerifyUserResponse = await CustomerUserEntityService.VerifyUserEntity(VerificationModel.UserID, userRepository);

                if(VerifyUserResponse is null) {
                    return InvalidToken;
                }

                var UserDtoResponse = mapper.Map<UserDto>(VerifyUserResponse);
                return new DefaultSuccessResponse<IUserEntityDto>(UserDtoResponse);

            }

            return new DefaultErrorResponse<IUserEntityDto>() {
                ResponseCode = ResponseCodes.NOT_FOUND,
                ResponseMessage = ResponseMessages.NOT_FOUND,
                ResponseData = null
            };

        }

    }
}
