namespace OrderUp_API.Models {
    public class MenuItem : AbstractEntity{

        [Required]
        [MaxLength(50)]
        public string MenuItemName { get; set; }

        [Required]
        [DataType(DataType.Currency)]
        [Precision(18, 2)]
        public decimal Price { get; set; }

        [ForeignKey("Restaurant")]
        public Guid RestaurantId { get; set; }

        [MaxLength(120)]
        public string Description { get; set; }

        [MaxLength(500)]
        public string? ImageUrl { get; set; }

        [ForeignKey("MenuCategory")]
        public Guid MenuCategoryID { get; set; }  

        //public virtual MenuCategory? MenuCategory { get; set; }

        //public virtual List<MenuItemImage>? MenuItemImages { get; set; }
    }
}
