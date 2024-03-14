namespace OrderUp_API.Interfaces.IUser {
    public class IUserEntity : AbstractEntity {

        [Required]
        [MaxLength(128)]
        public string Password { get; set; }

        [DataType(DataType.EmailAddress)]
        [Required]
        [MaxLength(100)]
        public string Email { get; set; }

        public bool IsEmailConfirmed { get; set; } = false;
    }
}
