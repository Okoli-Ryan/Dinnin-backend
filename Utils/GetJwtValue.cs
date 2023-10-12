namespace OrderUp_API.Utils {
    public class GetJwtValue {

        public static string GetValueFromBearerToken(HttpContext context, string key) {
            // Get the Authorization header from the request
            var authHeader = context.Request.Headers["Authorization"].FirstOrDefault();

            // Check if the header exists and starts with "Bearer "
            if (!string.IsNullOrEmpty(authHeader) && authHeader.StartsWith("Bearer ")) {
                // Get the token from the header (remove the "Bearer " prefix)
                var token = authHeader["Bearer ".Length..];

                // Parse the JWT token
                var jwt = new JwtSecurityTokenHandler().ReadJwtToken(token);

                // Check if the token contains a claim with the specified key
                var claim = jwt.Claims.FirstOrDefault(c => c.Type.Equals(key));

                // If a claim with the specified key was found, return its value
                if (claim != null) {
                    return claim.Value;
                }
            }

            // If the header doesn't exist or the token doesn't contain a claim with the specified key, return null
            return default;
        }


        public static string GetTokenFromCookie(HttpContext context, string key) {
            // Get the Authorization header from the request
            var token = context.Request.Cookies["Authorization"];

            // Check if the token exists
            if (!string.IsNullOrEmpty(token)) {

                // Parse the JWT token
                var jwt = new JwtSecurityTokenHandler().ReadJwtToken(token);

                // Check if the token contains a claim with the specified key
                var claim = jwt.Claims.FirstOrDefault(c => c.Type.Equals(key));

                // If a claim with the specified key was found, return its value
                if (claim != null) {
                    return claim.Value;
                }
            }

            // If the token doesn't exist or the token doesn't contain a claim with the specified key, return null
            return default;
        }
    }
}

// Convert Bearer Token Authorization to Cookies
// Setup frontend to send cookies instead