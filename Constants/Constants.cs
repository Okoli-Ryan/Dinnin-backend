namespace OrderUp_API.Constants {
    public interface TableModelConstants {

        public const int TableCodeLength = 32;
    }

    public interface OrderModelConstants {

        //Order Status 
        public const string INITIAL = "INITIAL";
        public const string PENDING = "PENDING";
        public const string COMPLETED = "COMPLETED";

        //Order Events
        public const string NEW_ORDER_EVENT = "NEW_ORDER_EVENT";

    }

    public interface MessageQueueTopics {

        public const string EMAIL = "Email";
        public const string FORGOT_PASSWORD = "Forgot Password";
        public const string PUSH_NOTIFICATION = "Push Notification";
        public const string STAFF_REGISTRATION = "Staff Registration";
    }

    public static class MessageQueueList {

        public static List<string> MESSAGE_QUEUE_LIST = new() {
               MessageQueueTopics.EMAIL,
               MessageQueueTopics.FORGOT_PASSWORD,
               MessageQueueTopics.PUSH_NOTIFICATION,
               MessageQueueTopics.STAFF_REGISTRATION
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
