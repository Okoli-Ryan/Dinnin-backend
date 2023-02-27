namespace OrderUp_API.DTOs {
    public class SidesDto : AbstractDto{

        public Guid menuItemId { get; set; }

        public MenuItem menuItem { get; set; }

        public List<SideItemDto> sideItems { get; set; }
    }
}
