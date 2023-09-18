namespace OrderUp_API.Interfaces {
    public interface IMessageProducerService {

        public void SendMessage<T>(string key, T message);

        public void Dispose();
    }
}
