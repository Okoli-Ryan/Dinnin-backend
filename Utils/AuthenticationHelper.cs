namespace OrderUp_API.Utils {
    public class AuthenticationHelper {

        public static string HashPassword(string Password) {

            if (String.IsNullOrEmpty(Password)) return "";
            return BCrypt.Net.BCrypt.HashPassword(Password);
        }

        public static bool VerifyPassword(string InputPassword, string StoredPassword) {
            return BCrypt.Net.BCrypt.Verify(InputPassword, StoredPassword);
        }
    }
}
