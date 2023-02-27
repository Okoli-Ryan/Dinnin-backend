namespace OrderUp_API.Classes {
    public class LoginResponse {

        public LoginResponse() { }

        public UserDto User { get; set; }

        public string Token { get; set; }

        public Double ExpiresAt { get; set; }
    }
}
