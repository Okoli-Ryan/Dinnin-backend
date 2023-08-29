namespace OrderUp_API.DTOs {
    public class RestaurantDto : AbstractDto {

        public RestaurantDto() {
            admins = new HashSet<AdminDto>();
        }

        public string restaurantName { get; set; }

        public string slug { get; set; }

        public string description { get; set; } 

        public decimal xCoordinate { get; set; }

        public decimal yCoordinate { get; set; }

        public string address { get; set; }

        public string contactPhoneNumber { get; set; }

        public string contactEmail { get; set; }

        public string logoUrl { get; set; }

        public string country { get; set; }

        public string state { get; set; }

        public string city { get; set; }

        public virtual ICollection<AdminDto> admins { get; set; }

        public List<MenuCategoryDto> categories { get; set; }
    }
}
