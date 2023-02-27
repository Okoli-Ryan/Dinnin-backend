namespace OrderUp_API.Interfaces {
    public interface IAbstractDto {

        public Guid id { get; set; }

        public bool activeStatus { get; set; }

        public DateTime createdAt { get; set; }

        public DateTime updatedAt { get; set; }
    }
}
