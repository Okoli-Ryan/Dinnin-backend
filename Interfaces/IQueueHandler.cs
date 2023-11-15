namespace OrderUp_API.Interfaces {
    public interface IQueueHandler {

        Task HandleMessageAsync(string Message);
    }

    public class IQueueMessage { }
}
