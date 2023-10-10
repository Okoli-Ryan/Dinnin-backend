namespace OrderUp_API.Constants {
    public interface TableModelConstants {

        public const int TableCodeLength = 20;
    }

    public interface OrderModelConstants {

        //Order Status 
        public const string INITIAL = "INITIAL";
        public const string PENDING = "PENDING";
        public const string COMPLETED = "COMPLETED";

        //Order Events
        public const string NEW_ORDER_EVENT = "NEW_ORDER_EVENT";
        
    }
}
