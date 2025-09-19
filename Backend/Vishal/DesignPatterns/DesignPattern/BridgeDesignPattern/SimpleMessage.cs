namespace BridgeDesignPattern
{
    /// <summary>
    /// A 'RefinedAbstraction' for sending a simple message.
    /// </summary>
    public class SimpleMessage : Message
    {
        public SimpleMessage(IMessageSender messageSender) : base(messageSender) { }

        public override void Send(string content)
        {
            Console.WriteLine("Sending Simple Message:");
            _messageSender.SendMessage(content);
        }
    }
}
