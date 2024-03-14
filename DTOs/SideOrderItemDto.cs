namespace OrderUp_API.DTOs {
    public class SideOrderItemDto : AbstractDto {

        public int quantity { get; set; }

        public Guid sideItemId { get; set; }

        public Guid sideOrderId { get; set; }

        public SideItemDto sideItem { get; set; }

        public SidesOrderDto sidesOrder { get; set; }
    }
}
