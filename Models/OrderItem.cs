namespace OrderUp_API.Models {
    public class OrderItem : AbstractEntity {


        [Required]
        public int Quantity { get; set; }

        [ForeignKey("MenuItem")]
        public Guid MenuItemID { get; set; }

        public virtual MenuItem? MenuItem { get; set; }

        [ForeignKey("Order")]
        public Guid OrderID { get; set; }

        public virtual Order? Order { get; set; }
    }
}
