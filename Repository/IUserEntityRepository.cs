namespace OrderUp_API.Repository {
    public class IUserEntityRepository<T> : AbstractRepository<T> where T : AbstractEntity {
        public IUserEntityRepository(OrderUpDbContext context) : base(context) {
        }

        public async Task<T> GetUserEntityByEmail<T>(string Email) where T : IUserEntity {
            return await context.Set<T>().Where(x => x.Email.Equals(Email)).FirstOrDefaultAsync();
        }

        public async Task<T> GetUserEntityByID<T>(Guid ID) where T : IUserEntity {
            return await context.Set<T>().Where(x => x.ID.Equals(ID)).FirstOrDefaultAsync();
        }

    }
}
