namespace OrderUp_API.Repository {
    public class AdminRepository : IUserEntityRepository<Admin> {

        public AdminRepository(OrderUpDbContext context) : base(context) { }

        public async Task<int> GetAdminEmailCount(string Email) {
            return await context.Admins.CountAsync(x => x.Email.Equals(Email));
        }

        public async Task<Admin> GetAdminByEmail(string Email) {

            return await context.Admins.Where(x => x.Email == Email).Include(x => x.Restaurant).AsNoTracking().FirstOrDefaultAsync();
        }


        public async Task<List<Admin>> GetAuthorizedPushNotificationRecipients(Guid RestaurantID) {

            return await context.Admins.Where(x => x.RestaurantID.Equals(RestaurantID)).AsNoTracking().ToListAsync();
        }

        public async Task<Admin> GetAdminByEmailAndRestaurantId(Guid RestaurantID, string Email) {

            return await context.Admins.Where(x => x.RestaurantID.Equals(RestaurantID) && x.Email.Equals(Email)).FirstOrDefaultAsync();
        }

        public async Task<PaginatedResponse<Admin>> GetAdminList(Guid restaurantID, PaginationRequest paginationRequest, string? name, string? email, string? phoneNumber) {

            var adminQuery = context.Admins.Where(x => x.RestaurantID.Equals(restaurantID)).AsQueryable();

            if (!string.IsNullOrEmpty(name)) {

                adminQuery = adminQuery.Where(x => x.FirstName.Contains(name) || x.LastName.Contains(name));
            }

            if (!string.IsNullOrEmpty(email)) {

                adminQuery = adminQuery.Where(x => x.Email.Equals(email));
            }

            if (!string.IsNullOrEmpty(phoneNumber)) {

                adminQuery = adminQuery.Where(x => x.PhoneNumber.Contains(phoneNumber));
            }

            return await GetPaginatedList(adminQuery, paginationRequest);

        }
    }
}
