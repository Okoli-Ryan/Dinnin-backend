using System.Security.Cryptography;

namespace OrderUp_API.Utils {
    public static class StringHash {
        public static int GenerateStringHashID(string text) {
            // Use a secure hashing algorithm (e.g., SHA-256)
            var sha256 = SHA256.Create();
            byte[] hashedBytes = sha256.ComputeHash(Encoding.UTF8.GetBytes(text));

            // Convert the first few bytes (adjust based on desired ID range)
            // to an integer using BitConverter (adjust byte order if needed)
            return Math.Abs(BitConverter.ToInt32(hashedBytes, 0));
        }
    }
}
