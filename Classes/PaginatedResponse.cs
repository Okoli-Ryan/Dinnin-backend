namespace OrderUp_API.Classes {
    public class PaginatedResponse<T> {

        public int Total {  get; set; }

        public int Page { get; set; }

        public int Size { get; set; }

        public List<T> Data { get; set; }
    }
}
