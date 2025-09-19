namespace BridgeDesignPattern
{
    /// <summary>
    /// The 'ConcreteImplementor' for sending messages via Email (e.g., using SendGrid API).
    /// </summary>
    public class EmailSender : IMessageSender
    {
        public void SendMessage(string message)
        {
            Console.WriteLine($"Sending Email: {message}");
        }
    }
}
