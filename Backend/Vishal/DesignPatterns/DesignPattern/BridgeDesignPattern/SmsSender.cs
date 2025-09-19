namespace BridgeDesignPattern
{
    /// <summary>
    /// The 'ConcreteImplementor' for sending messages via SMS (e.g., using Twilio API).
    /// </summary>
    public class SmsSender : IMessageSender
    {
        public void SendMessage(string message)
        {
            Console.WriteLine($"Sending SMS: {message}");
        }
    }
}
