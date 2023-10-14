using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using OrderUp_API.Constants;

namespace OrderUp_API.Services
{
    public class AdminService
    {

        private readonly AdminRepository adminRepository;
        private readonly IMapper mapper;
        private readonly OrderUpDbContext context;
        private readonly VerificationCodeService verificationCodeService;
        private readonly IMessageProducerService messageProducerService;
        private readonly HttpContext httpContext;

        public AdminService(AdminRepository adminRepository, IMapper mapper, OrderUpDbContext context, VerificationCodeService verificationCodeService, IMessageProducerService messageProducerService, IHttpContextAccessor httpContextAccessor)
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

            messageProducerService.SendMessage<EmailMQModel>("Email", new()
            {
                ID = ExistingAdmin.ID,
                Role = RoleTypes.Admin,
                Email = ExistingAdmin.Email
            });


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

                messageProducerService.SendMessage("Email", ExistingAdmin);

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
                new Claim(RestaurantIdentifier.RestaurantClaimType, ExistingAdmin.RestaurantID.ToString())
            };


            // Testing Cookie auth
            var claimsIdentity = new ClaimsIdentity(authClaims, CookieAuthenticationDefaults.AuthenticationScheme);

            var claimsPrincipal = new ClaimsPrincipal(claimsIdentity);

            await httpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, claimsPrincipal);




            //var token = JwtUtils.GenerateToken(authClaims).Token;

            //var httpCookieOptions = new CookieOptions()
            //{
            //    Expires = DateTime.Now.AddDays(1),
            //    HttpOnly = true,
            //    Secure = true
            //};

            //httpContext.Response.Cookies.Append("Authorization", token, httpCookieOptions);

            return new DefaultSuccessResponse<AdminDto>(ParseAdminResponse(ExistingAdmin));

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

        public async Task<List<AdminDto>> Save(List<Admin> admin)
        {

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
