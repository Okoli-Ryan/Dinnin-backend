namespace OrderUp_API.Repository {
    public class UserRepository : IUserEntityRepository<User> {

        public UserRepository(OrderUpDbContext context) : base(context) { }

    }
}
