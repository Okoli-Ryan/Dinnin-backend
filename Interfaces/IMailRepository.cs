namespace OrderUp_API.Interfaces {
    public interface IMailRepository {

        Task<bool> SendMail(List<string> receipients, string subject, string body, string contentType, string sender);
    }
}
