namespace OrderUp_API.Interfaces {
    public interface IMailService {

        Task<bool> SendMail(List<string> Receipients, string Subject, string Body, string ContentType, string sender);

        Task<bool> SendVerificationCode(string Receipient, string Code);
    }
}
