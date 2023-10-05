namespace OrderUp_API.Models {
    public class Order : AbstractEntity {

        [MaxLength(100)]
        public string OrderNote { get; set; }

        public decimal OrderAmount { get; set; }

        [ForeignKey("Restaurant")]
        public Guid RestaurantId { get; set; }

        [MaxLength(20)]
        public string PaymentOption { get; set; }

        [MaxLength(10)]
        public string OrderStatus { get; set; } = OrderModelConstants.INITIAL;

        public virtual List<OrderItem>? OrderItems { get; set; }

        public Guid? UserID { get; set; }

        public Guid? TableID { get; set; }


    }
}
