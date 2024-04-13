using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using OrderUp_API.Classes.ResponseDtos;
using OrderUp_API.Classes.ResponseModels;
using System.Text.RegularExpressions;
using System.Web.Helpers;

namespace OrderUp_API.Services {
    public class AdminService {

        private readonly AdminRepository adminRepository;
        private readonly VerificationCodeService verificationCodeService;
        private readonly IMapper mapper;
        private readonly MessageProducerService messageProducerService;
        private readonly HttpContext httpContext;
        private readonly RestaurantRepository restaurantRepository;
        private readonly AdminPermissionRepository adminPermissionRepository;

        public AdminService(AdminRepository adminRepository, VerificationCodeService verificationCodeService, RestaurantRepository restaurantRepository, IMapper mapper, MessageProducerService messageProducerService, IHttpContextAccessor httpContextAccessor, AdminPermissionRepository adminPermissionRepository) {
            this.adminRepository = adminRepository;
            this.mapper = mapper;
            this.messageProducerService = messageProducerService;
            this.restaurantRepository = restaurantRepository;
            this.verificationCodeService = verificationCodeService;
            this.adminPermissionRepository = adminPermissionRepository;
            httpContext = httpContextAccessor.HttpContext;
        }

        public async Task<object> Logout() {
            await httpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            return null;
        }

        public async Task<DefaultResponse<AdminDto>> RegisterAdmin(Admin Admin) {

            var ExistingAdmin = await adminRepository.GetAdminByEmail(Admin.Email);

            if (ExistingAdmin is not null) return new DefaultResponse<AdminDto>() {
                ResponseCode = ResponseCodes.USER_ALREADY_EXIST,
                ResponseMessage = ResponseMessages.USER_ALREADY_EXIST,
                ResponseData = null
            };

            Admin.Role = RoleTypes.SuperAdmin;
            Admin.IsEmailConfirmed = false;
            Admin.RecoveryEmail = Admin.Email;
            Admin.Position = "Owner";

            var CreatedAccount = await Save(Admin);

            if (CreatedAccount is null) return new DefaultErrorResponse<AdminDto>();


            messageProducerService.SendMessage(MessageQueueTopics.EMAIL, new EmailMQModel {
                ID = CreatedAccount.id,
                Role = RoleTypes.Admin,
                Email = CreatedAccount.recoveryEmail
            });

            return new DefaultSuccessResponse<AdminDto>(CreatedAccount);

        }


        public async Task<DefaultResponse<bool>> RegisterStaff(AdminDto AdminDto) {

            var Staff = mapper.Map<Admin>(AdminDto);

            var RestaurantID = GetJwtValue.GetGuidFromCookie(httpContext, RestaurantIdentifier.RestaurantID_ClaimType);

            var Restaurant = await restaurantRepository.GetByID(RestaurantID);

            if (Restaurant is null) return new DefaultNotFoundResponse<bool>("Unable to find restaurant");

            var staffEmail = ParseAdminName(Staff.FirstName, Staff.LastName) + $"@{Restaurant.Slug}.com";

            var numberOfAdminsWithSameEmail = await adminRepository.GetAdminEmailCount(staffEmail);

            if (numberOfAdminsWithSameEmail > 0) staffEmail = ParseAdminName(Staff.FirstName, Staff.LastName) + $"{numberOfAdminsWithSameEmail}@{Restaurant.Slug}.com";

            Staff.RecoveryEmail = AdminDto.emailAddress;
            Staff.Email = staffEmail;
            Staff.RestaurantID = RestaurantID;
            Staff.Role = RoleTypes.Admin;
            Staff.IsEmailConfirmed = true;
            Staff.Password = AuthenticationHelper.HashPassword(RandomStringGenerator.GenerateRandomString(10));

            var SavedStaff = await adminRepository.Save(Staff);

            if (SavedStaff is null) return new DefaultErrorResponse<bool>();

            messageProducerService.SendMessage(MessageQueueTopics.STAFF_REGISTRATION, new StaffRegistrationModel {
                Admin = Staff,
                RestaurantName = Restaurant.Name,
            });

            return new DefaultSuccessResponse<bool>(true);
        }


        public async Task<DefaultResponse<AdminDto>> LoginAsAdmin(LoginModel loginModel) {

            var ExistingAdmin = await adminRepository.GetAdminByEmail(loginModel.Email);

            var InvalidResponse = new DefaultErrorResponse<AdminDto>() {
                ResponseCode = ResponseCodes.INVALID_CREDENTIALS,
                ResponseMessage = ResponseMessages.INVALID_CREDENTIALS
            };


            if (ExistingAdmin is null) return InvalidResponse;


            var isPasswordCorrect = AuthenticationHelper.VerifyPassword(loginModel.Password, ExistingAdmin.Password);

            if (!isPasswordCorrect) return InvalidResponse;

            if (!ExistingAdmin.IsEmailConfirmed) {

                messageProducerService.SendMessage(MessageQueueTopics.EMAIL, new EmailMQModel {
                    ID = ExistingAdmin.ID,
                    Role = RoleTypes.Admin,
                    Email = ExistingAdmin.RecoveryEmail
                });

                return new DefaultErrorResponse<AdminDto>() {
                    ResponseCode = ResponseCodes.UNAUTHORIZED,
                    ResponseMessage = "A verification code was sent to you",
                    ResponseData = null
                };
            }

            var adminPermissions = await adminPermissionRepository.GetPermissionNamesByAdminID(ExistingAdmin.ID);

            var authClaims = new List<Claim>() {
                new Claim(ClaimTypes.Role, ExistingAdmin.Role),
                new Claim(ClaimTypes.Email, ExistingAdmin.Email),
                new Claim(ClaimTypes.PrimarySid, ExistingAdmin.ID.ToString()),
                new Claim(RestaurantIdentifier.RestaurantID_ClaimType, ExistingAdmin.RestaurantID.ToString()),
            };

            foreach (var permission in adminPermissions) {
                authClaims.Add(new Claim(ClaimType.PERMISSION_CLAIM_TYPE, permission));
            }


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

        //Add code to payload and verify
        //Remove user id
        public async Task<DefaultResponse<bool>> HandleResetPassword(string Code, string newPassword) {

            var VerificationCode = await verificationCodeService.VerifyVerificationCode(Code);

            if (VerificationCode is null) return new DefaultInvalidTokenResponse<bool>("Verification code is invalid");

            var Admin = await adminRepository.GetByID(VerificationCode.UserID);

            if (Admin is null) return new DefaultNotFoundResponse<bool>("Unable to find user");

            Admin.Password = AuthenticationHelper.HashPassword(newPassword);

            var UpdatedAdmin = await adminRepository.Update(Admin);

            if (UpdatedAdmin is null) return new DefaultFailureResponse<bool>();

            return new DefaultSuccessResponse<bool>(true);

        }

        public async Task<DefaultResponse<PaginatedResponse<AdminDto>>> GetAdminList(string? name, string? email, string? phoneNumber, int page, int pageSize, DateTime? startDate, DateTime? EndDate) {

            var paginationRequest = new PaginationRequest(page, pageSize, startDate, EndDate);

            var restaurantID = GetJwtValue.GetGuidFromCookie(httpContext, RestaurantIdentifier.RestaurantID_ClaimType);

            if (restaurantID == Guid.Empty) {
                return new DefaultUnauthorizedResponse<PaginatedResponse<AdminDto>>();
            }

            var PaginatedAdminsResponse = await adminRepository.GetAdminList(restaurantID, paginationRequest, name, email, phoneNumber);

            if (PaginatedAdminsResponse is null) return new DefaultFailurePaginationResponse<AdminDto>();

            var mappedData = mapper.Map<List<AdminDto>>(PaginatedAdminsResponse.Data);

            var response = new PaginatedResponse<AdminDto>() {
                Data = mappedData,
                Page = PaginatedAdminsResponse.Page,
                Size = PaginatedAdminsResponse.Size,
                Total = PaginatedAdminsResponse.Total
            };

            return new DefaultSuccessResponse<PaginatedResponse<AdminDto>>(response);
        }



        public async Task<DefaultResponse<GetPermissionsByAdminDto>> GetAdminPermissions(Guid adminId) {


            var adminPermissionsTask = adminPermissionRepository.GetPermissionsByAdminID(adminId);

            var adminNameTask = adminPermissionRepository.GetAdminNameByID(adminId);

            await Task.WhenAll(adminPermissionsTask, adminNameTask);

            var adminName = await adminNameTask;
            var adminPermissions = await adminPermissionsTask;

            var response = new GetPermissionsByAdminResponse {
                AdminName = $"{adminName.FirstName} {adminName.LastName}",
                PermissionGroups = adminPermissions
            };


            var mappedResponse = mapper.Map<GetPermissionsByAdminDto>(response);

            return new DefaultSuccessResponse<GetPermissionsByAdminDto>(mappedResponse);

        }




        public async Task<DefaultResponse<bool>> UpdateAdminPermissions(Guid adminId, List<int> permissionIds) {

            var response = await adminPermissionRepository.UpdateAdminPermissions(adminId, permissionIds);

            if (!response) return new DefaultErrorResponse<bool>();

            return new DefaultSuccessResponse<bool>(response);
        }


        public async Task<List<Admin>> GetAuthorizedPushNotificationRecipients(Guid RestaurantID) {

            var recipients = await adminRepository.GetAuthorizedPushNotificationRecipients(RestaurantID);

            return recipients;

        }

        public async Task<AdminDto> Save(Admin admin) {

            admin.Password = AuthenticationHelper.HashPassword(admin.Password);

            var addedAdmin = await adminRepository.Save(admin);

            return ParseAdminResponse(addedAdmin);
        }


        public async Task<AdminDto> GetByID(Guid ID) {

            var admin = await adminRepository.GetByID(ID);

            return mapper.Map<AdminDto>(admin);
        }

        public async Task<DefaultResponse<AdminDto>> Update(AdminDto admin) {

            var mappedAdmin = mapper.Map<Admin>(admin);

            var updatedAdmin = await adminRepository.Update(mappedAdmin);

            if (updatedAdmin is null) return new DefaultErrorResponse<AdminDto>();

            var mappedResponse = mapper.Map<AdminDto>(updatedAdmin);

            return new DefaultSuccessResponse<AdminDto>(mappedResponse);
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

        public static string ParseAdminName(string firstName, string lastName) {
            // Remove special characters and spaces using regular expressions
            string strippedFirstName = Regex.Replace(firstName, @"[^\w\d]", "");
            string strippedLastName = Regex.Replace(lastName, @"[^\w\d]", "");

            // Concatenate the stripped names
            string concatenatedNames = strippedFirstName + strippedLastName;

            // Convert to lowercase
            concatenatedNames = concatenatedNames.ToLower();

            return concatenatedNames;
        }

    }
}
