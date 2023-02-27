namespace OrderUp_API.DTOs {
    public class MenuItemDto : AbstractDto{

        public string menuItemName { get; set; }

        public decimal price { get; set; }

        public Guid menuCategoryId { get; set; }

        public Guid restaurantId { get; set; }

        public string imageUrl { get; set; }

        public string description { get; set; }

        //public MenuCategory menuCategory { get; set; }

        //public List<MenuItemImageDto> images { get; set; }
    }
}
