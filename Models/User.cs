namespace OrderUp_API.Models {
    [Microsoft.EntityFrameworkCore.Index(nameof(Email), IsUnique = true)]
    public class User : IUserEntity {

        [Required]
        [MaxLength(50)]
        public string FirstName { get; set; }

        [MaxLength(50)]
        public string LastName { get; set; }

        [MaxLength(50)]
        [Required]
        [DataType(DataType.PhoneNumber)]
        public string PhoneNumber { get; set; }


        [DataType(DataType.ImageUrl)]
        [MaxLength(500)]
        public string? UserImageUrl { get; set; }

        public virtual List<SavedRestaurant>? SavedRestaurants { get; set; }
    }
}
