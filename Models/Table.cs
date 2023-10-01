namespace OrderUp_API.Models {
    public class Table : AbstractEntity{

        [MaxLength(50)]
        public string TableName { get; set; }

        [MaxLength(TableModelConstants.TableCodeLength)]
        public string? Code { get; set; }

        [ForeignKey("Restaurant")]
        public Guid RestaurantID { get; set; }

        public virtual Restaurant? Restaurant { get; set; }
    }
}
