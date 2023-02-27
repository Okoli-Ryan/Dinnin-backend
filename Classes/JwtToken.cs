namespace OrderUp_API.Classes {
    public class JwtToken {

        public JwtToken() { }

        public string Token { get; set; }

        public Double ExpiresAt { get; set; }
    }
}
