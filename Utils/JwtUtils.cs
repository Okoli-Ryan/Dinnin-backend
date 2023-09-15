namespace OrderUp_API.Utils {
    public class JwtUtils {


        public JwtUtils() { 
        }

        public static JwtToken GenerateToken(List<Claim> authClaims) {

            var defaultClaims = new List<Claim>() {
                    new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
            };

            foreach (var claim in authClaims) { 
                defaultClaims.Add(claim);
            }

            return GetToken(authClaims);

        }

        public static JwtSecurityToken DecodeJWT(string jwt) {
            var handler = new JwtSecurityTokenHandler();

            var token = handler.ReadJwtToken(jwt);

            return token;
        }

        public static JwtToken GetToken(List<Claim> authClaims) {

            var Secret = ConfigurationUtil.GetConfigurationValue("Jwt:Secret");

            var authSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(Secret));

            var ExpiresAt = TimeSpan.FromDays(1);

            var token = new JwtSecurityToken(
                issuer: ConfigurationUtil.GetConfigurationValue("Jwt:Issuer"),
                audience: ConfigurationUtil.GetConfigurationValue("Jwt:Audience"),
                expires: DateTime.Now.Add(ExpiresAt),
                claims: authClaims,
                signingCredentials: new SigningCredentials(authSigningKey, SecurityAlgorithms.HmacSha256)
                );

            return new JwtToken() {
                Token = new JwtSecurityTokenHandler().WriteToken(token),
                ExpiresAt = ExpiresAt.TotalSeconds
            };
        }
    }
}
