namespace OrderUp_API.Models {
    public class SideItem : AbstractEntity {

        [Required]
        [MaxLength(100)]
        public string SideItemName { get; set; }

        [DataType(DataType.Currency)]
        [Required]
        [Precision(18, 2)]
        public decimal SideItemPrice { get; set; }

        [ForeignKey("Sides")]
        public Guid SidesID { get; set; }

        public virtual Sides? Sides { get; set; }
    }
}
