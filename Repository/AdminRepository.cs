namespace OrderUp_API.Repository {
    public class AdminRepository : IUserEntityRepository<Admin>{

        public AdminRepository(OrderUpDbContext context) : base(context) { }

    }
}
