namespace OrderUp_API.Services {
    public class IUserEntityService<T> where T : IUserEntity {

        private readonly IUserEntityRepository<T> UserEntityRepository;
        private readonly IMapper Mapper;

        public IUserEntityService(IUserEntityRepository<T> UserEntityRepository, IMapper Mapper) {
            this.UserEntityRepository = UserEntityRepository;
            this.Mapper = Mapper;
        }

        public async Task<T> GetUserEntityByEmail(string Email) {
            var UserEntity = await UserEntityRepository.GetUserEntityByEmail<T>(Email);

            return UserEntity;
        }


        public async Task<T> GetUserEntityByID(Guid ID)  {
            var User = await UserEntityRepository.GetUserEntityByID<T>(ID);

            return User;
        }

        public async Task<T> VerifyUserEntity<K>(Guid VerificationID, K repository) where K : IUserEntityRepository<T> {


            T UserEntity = await GetUserEntityByID(VerificationID);

            UserEntity.IsEmailConfirmed = true;

            if (UserEntity == null) {
                return null;
            }

            var UpdatedUserEntity = await repository.Update(UserEntity);

            if (UpdatedUserEntity == null) {
                return null;
            }

            UpdatedUserEntity.Password = null;

            return UpdatedUserEntity;

        }

        //public async Task<K> VerifyUserEntityByID<T, K>(Guid ID) where T : IUserEntity where K : IUserEntityDto {
        //    var UserEntity = await UserEntityRepository.GetUserEntityByID<T>(ID);

        //    User.Password = null;

        //    return Mapper.Map<K>(User);
        //}

    }
}
