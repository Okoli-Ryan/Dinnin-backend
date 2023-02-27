namespace OrderUp_API.Models {
    public class MenuItemImage : AbstractEntity {

        [Required]
        [MaxLength(500)]
        public string ImageUrl { get; set; }

        [ForeignKey("MenuItem")]
        public Guid MenuItemID { get; set; }

        public virtual MenuItem? MenuItem { get; set; }
    }
}
