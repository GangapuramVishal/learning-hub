namespace BridgeDesignPattern
{
    /// <summary>
    /// A 'RefinedAbstraction' for sending an urgent message.
    /// </summary>
    public class UrgentMessage : Message
    {
        public UrgentMessage(IMessageSender messageSender) : base(messageSender) { }

        public override void Send(string content)
        {
            Console.WriteLine("Sending Urgent Message:");
            _messageSender.SendMessage("[URGENT] " + content);
        }
    }
}
