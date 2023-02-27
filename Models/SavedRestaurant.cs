namespace OrderUp_API.Models {
    public class SavedRestaurant : AbstractEntity{

        [ForeignKey("Restaurant")]
        public Guid RestaurantID { get; set; }

        [ForeignKey("User")]
        public Guid UserID { get; set; }

        public virtual Restaurant? Restaurant { get; set; }

        public virtual User? User { get; set; }
    }
}
