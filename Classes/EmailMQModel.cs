namespace OrderUp_API.Classes {
    public class EmailMQModel : IQueueMessage {

        public Guid ID { get; set; }

        public string Email { get; set; }

        public string Role { get; set; }
    }
}
