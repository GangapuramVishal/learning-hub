using RabbitMQ.Client;
using System.Text;

namespace Sender
{
    public class Program
    {
        static void Main()
        {
            // Get RabbitMQ hostname from environment variables
            string rabbitMqHost = Environment.GetEnvironmentVariable("RABBITMQ_HOST") ?? "localhost";

            var factory = new ConnectionFactory()
            {
                HostName = rabbitMqHost,
                Port = 5672, // Default RabbitMQ port
                UserName = "guest",
                Password = "guest"
            };

            using (var connection = factory.CreateConnection())
            using (var channel = connection.CreateModel())
            {
                channel.QueueDeclare(queue: "hello",
                                     durable: false,
                                     exclusive: false,
                                     autoDelete: false,
                                     arguments: null);

                string message = "Hello from SenderApp, RabbitMQ!";
                var body = Encoding.UTF8.GetBytes(message);

                channel.BasicPublish(exchange: "",
                                     routingKey: "hello",
                                     basicProperties: null,
                                     body: body);

                Console.WriteLine($"[x] Sent: {message}");
            }
        }
    }
}
