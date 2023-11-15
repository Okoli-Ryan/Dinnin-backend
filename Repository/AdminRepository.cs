namespace OrderUp_API.Repository {
    public class AdminRepository : IUserEntityRepository<Admin>{

        public AdminRepository(OrderUpDbContext context) : base(context) { }

        public async Task<Admin> GetAdminByEmail(string Email) {

            return await context.Admins.Where(x => x.Email == Email).Include(x => x.Restaurant).AsNoTracking().FirstOrDefaultAsync();
        }
        
        
        public async Task<List<Admin>> GetAuthorizedPushNotificationRecipients(Guid RestaurantID) {

            return await context.Admins.Where(x => x.RestaurantID.Equals(RestaurantID)).AsNoTracking().ToListAsync();
        }

    }
}
