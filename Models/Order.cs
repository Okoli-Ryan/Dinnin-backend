namespace OrderUp_API.Models {
    public class Order : AbstractEntity {

        public Order() {
            OrderStatus = string.IsNullOrEmpty(OrderStatus) ? OrderModelConstants.INITIAL : OrderStatus;
        }

        [MaxLength(100)]
        public string? OrderNote { get; set; }

        public decimal? OrderAmount { get; set; }

        [ForeignKey("Restaurant")]
        public Guid RestaurantId { get; set; }

        [MaxLength(20)]
        public string PaymentOption { get; set; }

        [MaxLength(10)]
        public string? OrderStatus { get; set; }

        public Guid? UserID { get; set; }

        public Guid? TableID { get; set; }

        public virtual List<OrderItem>? OrderItems { get; set; }

        public virtual Table? Table { get; set; }

    }
}
