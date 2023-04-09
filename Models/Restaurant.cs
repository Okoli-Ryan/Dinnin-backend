
using Microsoft.EntityFrameworkCore.Metadata.Internal;

namespace OrderUp_API.Models {
    [Microsoft.EntityFrameworkCore.Index(nameof(Slug), IsUnique = true)]
    public class Restaurant : AbstractEntity{

        public Restaurant() {
            Admins = new HashSet<Admin>();
        }

        [Required]
        [MaxLength(50)]
        public string Name { get; set; }

        [MaxLength(500)]
        public string Description { get; set; }

        [Required]
        [MaxLength(50)]
        public string Slug { get; set; }

        [Precision(18, 5)]
        public decimal XCoordinate { get; set; }

        [Precision(18, 5)]
        public decimal YCoordinate { get; set; }

        [Required]
        [MaxLength(100)]
        public string Address { get; set; }

        [Required]
        [DataType(DataType.PhoneNumber)]
        [MaxLength(20)]
        public string ContactPhoneNumber { get; set; }

        [MaxLength(500)]
        [DataType(DataType.ImageUrl)]
        public string LogoUrl { get; set; }

        [Required]
        [MaxLength(100)]
        [DataType(DataType.EmailAddress)]
        public string ContactEmailAddress { get; set; }

        [Required]
        [MaxLength(100)]
        public string Country { get; set; }

        [Required]
        [MaxLength(100)]
        public string State { get; set; }

        [Required]
        [MaxLength(100)]
        public string City { get; set; }


        public virtual ICollection<Admin> Admins { get; set; }


        public virtual List<MenuCategory>? MenuCategories { get; set; }
    }
}
