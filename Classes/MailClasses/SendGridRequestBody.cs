namespace OrderUp_API.Classes.MailClasses {
    public class EmailRequestBody {

        public string Subject { get; set; }

        public List<Personalization> Personalizations { get; set; }
        public EmailUser From { get; set; }

        public List<EmailContent> Content { get; set; }

    }

    public class Personalization {

        public List<EmailUser> To { get; set; }
        public Personalization(List<string> Emails) {
            To = Emails.Select(email => new EmailUser { Email = email }).ToList();
        }

    }

    public class EmailUser {

        
        public string Email { get; set; }
    }

    public class EmailContent {
        public string Type { get; set; }

        public string Value { get; set; }
    }
}
