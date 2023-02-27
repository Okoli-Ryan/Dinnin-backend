namespace OrderUp_API.Interfaces.IUser {
    public class IUserEntityDto : AbstractDto {

        public string emailAddress { get; set; }

        public bool isEmailConfirmed { get; set; }

        public string password { get; set; }
    }
}
