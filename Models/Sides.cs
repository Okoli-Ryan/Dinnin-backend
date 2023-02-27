namespace OrderUp_API.Models {
    public class Sides : AbstractEntity {

        [ForeignKey("MenuItem")]
        public Guid MenuItemID { get; set; }

        public virtual MenuItem? MenuItem { get; set; }

        public virtual List<SideItem>? SideItems { get; set; }
    }
}
