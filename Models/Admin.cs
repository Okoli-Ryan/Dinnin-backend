using OrderUp_API.Interfaces.IUser;

namespace OrderUp_API.Models {
    [Table(name: "Admin")]
    public class Admin : IUserEntity{

        public Admin() { 
            Restaurant = new Restaurant();
        }

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
