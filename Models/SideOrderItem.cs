namespace OrderUp_API.Models {
    public class SideOrderItem : AbstractEntity {

        public int Quantity { get; set; }

        [ForeignKey("SideItem")]
        public Guid SideItemID { get; set; }

        [ForeignKey("SideOrder")]
        public Guid SideOrderID { get; set; }

        public virtual SideItem? SideItem { get; set; }

        public virtual SidesOrder? SidesOrder { get; set; }

    }
}
