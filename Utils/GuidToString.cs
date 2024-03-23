namespace OrderUp_API.Utils {
    public static class GuidStringConverter {
        public static string GuidToString(Guid value) {
            if (Guid.TryParse(value.ToString(), out Guid restaurantId)) {
                return restaurantId.ToString();
            }

            return null;
        }

        public static Guid StringToGuid(string input) {
            if (Guid.TryParse(input, out Guid result)) {
                return result;
            }

            return Guid.Empty;
        }

    }



}
