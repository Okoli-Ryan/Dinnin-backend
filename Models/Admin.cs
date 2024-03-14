namespace OrderUp_API.Models {
    [Table(name: "Admin")]
    public class Admin : IUserEntity {

        [Required]
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


    }
}
