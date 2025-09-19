using Azure.Messaging.ServiceBus;

namespace Sender_Application
{
    public class Program
    {
        private const string ServiceBusConnectionString = "Endpoint=sb://demoondocker.servicebus.windows.net/;SharedAccessKeyName=RootManageSharedAccessKey;SharedAccessKey=";
        private const string QueueName = "docker";

        public static async Task Main()
        {
            // Create a Service Bus client
            await using var client = new ServiceBusClient(ServiceBusConnectionString);

            // Create a sender for the queue
            ServiceBusSender sender = client.CreateSender(QueueName);

            // Create a message to send
            string messageBody = "Hello from SenderApp, Azure Service Bus!";
            ServiceBusMessage message = new ServiceBusMessage(messageBody);

            // Send the message to the queue
            await sender.SendMessageAsync(message);
            Console.WriteLine($"[x] Sent: {messageBody}");
        }
    }
}
