namespace OrderUp_API.Classes {
    public class AdminLoginResponse {

        public AdminLoginResponse() { }

        public AdminDto Admin { get; set; }

        public string Token { get; set; }

        public double ExpiresAt { get; set; }
    }
}