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

    public static class MessageQueueList {

        public static List<string> MESSAGE_QUEUE_LIST = new() {
               MessageQueueTopics.EMAIL,
               MessageQueueTopics.FORGOT_PASSWORD,
               MessageQueueTopics.PUSH_NOTIFICATION
        };

        public static List<string> getQueue() {
            return MESSAGE_QUEUE_LIST;
        }
    }

    public interface AnalyticsConstants {

        public const string GROUP_BY_DATE = "date";
        public const string GROUP_BY_DAY = "day";
        public const string GROUP_BY_WEEK = "week";
        public const string GROUP_BY_MONTH = "month";

    }

}
