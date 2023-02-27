namespace OrderUp_API.DTOs {
    public class SideItemDto : AbstractDto {

        public string sideItemName { get; set; }

        public decimal sideItemPrice { get; set; }

        public Guid sidesId { get; set; }

        public Sides sides { get; set; }
    }
}
