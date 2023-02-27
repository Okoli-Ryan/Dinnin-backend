namespace OrderUp_API.DTOs {
    public class MenuItemImageDto : AbstractDto {

        public string imageUrl { get; set; }

        public Guid menuItemID { get; set; }

        public MenuItemDto menuItem { get; set; }

    }
}
