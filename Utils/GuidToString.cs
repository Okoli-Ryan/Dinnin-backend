namespace OrderUp_API.Utils {
    public static class GuidToString {
        public static string Convert(Guid value) {
            if (Guid.TryParse(value.ToString(), out Guid restaurantId)) {
                return restaurantId.ToString();
            }

            return null;
        }
    }

}
