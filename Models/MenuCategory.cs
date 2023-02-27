namespace OrderUp_API.Models {
    public class MenuCategory : AbstractEntity{

        [Required]
        [MaxLength(50)]
        public string CategoryName { get; set; }

        [ForeignKey("Restaurant")]
        public Guid RestaurantID { get; set; }

        //public virtual Restaurant? Restaurant { get; set; }

        public virtual List<MenuItem>? MenuItems { get; set; }
    }
}
