namespace OrderUp_API.DTOs {
    public class VerificationCodeDto : AbstractDto {

        public string code { get; set; }

        public Guid userId { get; set; }

        public DateTime expiryDate { get; set; }

        public string userType { get; set; }
    }
}
