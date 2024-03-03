using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using OrderUp_API.Constants;
using OrderUp_API.Models;

namespace OrderUp_API.Services
{
    public class AdminService
    {

        private readonly AdminRepository adminRepository;
        private readonly IMapper mapper;
        private readonly OrderUpDbContext context;
        private readonly VerificationCodeService verificationCodeService;
        private readonly MessageProducerService messageProducerService;
        private readonly HttpContext httpContext;

        public AdminService(AdminRepository adminRepository, IMapper mapper, OrderUpDbContext context, VerificationCodeService verificationCodeService, MessageProducerService messageProducerService, IHttpContextAccessor httpContextAccessor)
        {
            this.adminRepository = adminRepository;
            this.mapper = mapper;
            this.context = context;
            this.verificationCodeService = verificationCodeService;
            this.messageProducerService = messageProducerService;
            this.httpContext = httpContextAccessor.HttpContext;
        }

        public async Task<DefaultResponse<AdminDto>> RegisterAdmin(Admin Admin)
        {

            var ExistingAdmin = await GetAdminByEmail(Admin.Email);

            if (ExistingAdmin is not null) return new DefaultResponse<AdminDto>()
            {
                ResponseCode = ResponseCodes.USER_ALREADY_EXIST,
                ResponseMessage = ResponseMessages.USER_ALREADY_EXIST,
                ResponseData = null
            };


            Admin.Role = RoleTypes.SuperAdmin;
            Admin.IsEmailConfirmed = false;

            using var transaction = context.Database.BeginTransaction();

            var CreatedAccount = await Save(Admin);


            messageProducerService.SendMessage(MessageQueueTopics.EMAIL, new EmailMQModel
            {
                ID = CreatedAccount.id,
                Role = RoleTypes.Admin,
                Email = CreatedAccount.emailAddress
            });

            transaction.Commit();

            return new DefaultSuccessResponse<AdminDto>(CreatedAccount);

        }


        public async Task<DefaultResponse<AdminDto>> LoginAsAdmin(LoginModel loginModel)
        {

            var ExistingAdmin = await GetAdminByEmail(loginModel.Email);

            var InvalidResponse = new DefaultErrorResponse<AdminDto>()
            {
                ResponseCode = ResponseCodes.INVALID_CREDENTIALS,
                ResponseMessage = ResponseMessages.INVALID_CREDENTIALS
            };


            if (ExistingAdmin is null) return InvalidResponse;

            
            var isPasswordCorrect = AuthenticationHelper.VerifyPassword(loginModel.Password, ExistingAdmin.Password);

            if (!isPasswordCorrect) return InvalidResponse;

            if (!ExistingAdmin.IsEmailConfirmed)
            {

                messageProducerService.SendMessage(MessageQueueTopics.EMAIL, new EmailMQModel
                {
                    ID = ExistingAdmin.ID,
                    Role = RoleTypes.Admin,
                    Email = ExistingAdmin.Email
                });

                return new DefaultErrorResponse<AdminDto>()
                {
                    ResponseCode = ResponseCodes.UNAUTHORIZED,
                    ResponseMessage = "A verification code was sent to you",
                    ResponseData = null
                };
            }




            var authClaims = new List<Claim>() {
                new Claim(ClaimTypes.Role, ExistingAdmin.Role),
                new Claim(ClaimTypes.Email, ExistingAdmin.Email),
                new Claim(ClaimTypes.PrimarySid, ExistingAdmin.ID.ToString()),
                new Claim(RestaurantIdentifier.RestaurantID_ClaimType, ExistingAdmin.RestaurantID.ToString())
            };


            var claimsIdentity = new ClaimsIdentity(authClaims, CookieAuthenticationDefaults.AuthenticationScheme);

            var claimsPrincipal = new ClaimsPrincipal(claimsIdentity);

            await httpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, claimsPrincipal);

            return new DefaultSuccessResponse<AdminDto>(ParseAdminResponse(ExistingAdmin));

        }

        public async Task<DefaultResponse<bool>> HandleForgotPasswordRequest(string email) {

            var existingAdmin = await adminRepository.GetAdminByEmail(email);

            if (existingAdmin is null) return new DefaultErrorResponse<bool> {
                ResponseCode = ResponseCodes.NOT_FOUND,
                ResponseMessage = "User doesn't exist",
                ResponseData = false
            };


            messageProducerService.SendMessage(MessageQueueTopics.FORGOT_PASSWORD, new EmailMQModel {
                ID = existingAdmin.ID,
                Role = RoleTypes.Admin,
                Email = existingAdmin.Email
            });

            return new DefaultSuccessResponse<bool>(true);
        }


        public async Task<DefaultResponse<bool>> HandleResetPassword(Guid UserID, string newPassword) {

            var Admin = await GetByID(UserID);

            if(Admin is null) {

                return new DefaultNotFoundResponse<bool>();
            }


            Admin.password = AuthenticationHelper.HashPassword(newPassword);

            var UpdatedAdmin = await Update(Admin);

            if(UpdatedAdmin is null) return new DefaultFailureResponse<bool>();

            return new DefaultSuccessResponse<bool>(true);

        }


        public async Task<List<Admin>> GetAuthorizedPushNotificationRecipients(Guid RestaurantID) {

            var recipients = await adminRepository.GetAuthorizedPushNotificationRecipients(RestaurantID);

            return recipients;

        }

        public async Task<Admin> GetAdminByEmail(string Email)
        {

            return await adminRepository.GetAdminByEmail(Email);
        }

        public async Task<AdminDto> Save(Admin admin)
        {

            admin.Password = AuthenticationHelper.HashPassword(admin.Password);

            var addedAdmin = await adminRepository.Save(admin);

            return ParseAdminResponse(addedAdmin);
        }


        public async Task<AdminDto> GetByID(Guid ID)
        {

            var admin = await adminRepository.GetByID(ID);

            return mapper.Map<AdminDto>(admin);
        }

        public async Task<DefaultResponse<AdminDto>> Update(AdminDto admin)
        {

            var mappedAdmin = mapper.Map<Admin>(admin);

            var updatedAdmin = await adminRepository.Update(mappedAdmin);

            if (updatedAdmin is null) return new DefaultErrorResponse<AdminDto>();

            var mappedResponse = mapper.Map<AdminDto>(updatedAdmin);

            return new DefaultSuccessResponse<AdminDto>(mappedResponse);
        }

        public async Task<bool> Delete(Guid ID)
        {

            return await adminRepository.Delete(ID);
        }

        public async Task<bool> Delete(List<Admin> admin)
        {

            return await adminRepository.Delete(admin);
        }

        public AdminDto ParseAdminResponse(Admin admin)
        {

            var response = mapper.Map<AdminDto>(admin);
            response.password = null;
            return response;
        }

        public List<AdminDto> ParseAdminResponse(List<Admin> admins)
        {

            var response = mapper.Map<List<AdminDto>>(admins);

            foreach (var admin in response)
            {
                admin.password = null;
            }
            return response;
        }
    }
}
