namespace OrderUp_API.Models {
    [Table(name: "Admin")]
    public class Admin : IUserEntity {

        [MaxLength(16)]
        public string Role { get; set; }

        [Required]
        [MaxLength(50)]
        public string FirstName { get; set; }

        [MaxLength(50)]
        public string LastName { get; set; }

        public Guid? RestaurantID { get; set; }

        public virtual Restaurant? Restaurant { get; set; }

        [MaxLength(50)]
        [Required]
        [DataType(DataType.PhoneNumber)]
        public string PhoneNumber { get; set; }

        [DataType(DataType.EmailAddress)]
        [MaxLength(100)]
        public string RecoveryEmail { get; set; }

        [MaxLength(32)]
        public string Position { get; set; }

        public List<AdminPermission> AdminPermissions { get; set; } = new();

        public List<Permission> Permissions { get; set; } = new();
    }
}
