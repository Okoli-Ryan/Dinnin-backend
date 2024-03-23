using System.Drawing.Printing;

namespace OrderUp_API.Classes {
    public class PaginatedResponse<T> {

        public int Total {  get; set; }

        public int Page { get; set; }

        public int Size { get; set; }

        public List<T> Data { get; set; }

        public static readonly PaginatedResponse<T> DefaultPaginatedResponse = new() {
            Data = new List<T>(),
            Page = 1,
            Size = 10,
            Total = 0
        };

    }

    public class PaginationRequest {
        public int Page { get; set; } = 1;

        public int Size { get; set; } = 10;

        public DateTime? MinCreatedAt { get; set; }

        public DateTime? MaxCreatedAt { get; set; }

        public PaginationRequest(int page = 1, int size = 10, DateTime? minCreatedAt = null, DateTime? maxCreatedAt = null) {
            Page = page <= 0 ? 1 : page;
            Size = size <= 0 ? 10 : size;
            MinCreatedAt = minCreatedAt ?? DateTime.MinValue;
            MaxCreatedAt = maxCreatedAt ?? DateTime.Now;
        }

    }
}
