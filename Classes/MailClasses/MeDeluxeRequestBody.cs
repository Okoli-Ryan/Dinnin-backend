namespace OrderUp_API.Classes.MailClasses {
    public class MeDeluxeRequestBody {

        public string Name { get; set; }
        public string Subject { get; set; }
        public string To { get; set; }
        public string From { get; set; }
        public string Content { get; set; }
        public string Html { get; set; }
    }
}
