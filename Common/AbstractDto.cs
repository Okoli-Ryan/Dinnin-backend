namespace OrderUp_API.Common {
    public class AbstractDto : IAbstractDto {

        public Guid id { get; set; }

        public bool activeStatus { get; set; }

        public DateTime createdAt { get; set; }

        public DateTime updatedAt { get; set; }
    }
}
