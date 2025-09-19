namespace BridgeDesignPattern
{
    public class Program
    {
        static void Main(string[] args)
        {
            // Create platform-specific senders
            IMessageSender smsSender = new SmsSender();
            IMessageSender emailSender = new EmailSender();

            // Send a simple message via SMS
            Message simpleSms = new SimpleMessage(smsSender);
            simpleSms.Send("Hello! This is a simple SMS.");

            // Send an urgent message via Email
            Message urgentEmail = new UrgentMessage(emailSender);
            urgentEmail.Send("Server down! Immediate attention needed.");
        }
    }
}
