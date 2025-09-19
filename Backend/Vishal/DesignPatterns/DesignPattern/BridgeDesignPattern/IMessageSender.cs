namespace BridgeDesignPattern
{
    /// <summary>
    /// The 'Implementor' interface defines the operations that need to be implemented by platform-specific classes.
    /// </summary>
    public interface IMessageSender
    {
        /// <summary>
        /// Sends a message using a specific platform.
        /// </summary>
        /// <param name="message">The message to be sent.</param>
        void SendMessage(string message);
    }
}
