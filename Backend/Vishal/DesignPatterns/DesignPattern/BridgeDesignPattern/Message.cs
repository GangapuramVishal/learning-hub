namespace BridgeDesignPattern
{
    /// <summary>
    /// The 'Abstraction' defines the high-level interface for sending messages.
    /// It uses a platform-specific sender (IMessageSender) to send the message.
    /// </summary>
    public abstract class Message
    {
        protected IMessageSender _messageSender;

        /// <summary>
        /// Constructor to initialize the message sender platform.
        /// </summary>
        /// <param name="messageSender">The platform-specific message sender.</param>
        protected Message(IMessageSender messageSender)
        {
            _messageSender = messageSender;
        }

        /// <summary>
        /// Sends the message. This method will be implemented by concrete abstractions.
        /// </summary>
        /// <param name="content">The message content.</param>
        public abstract void Send(string content);
    }
}
