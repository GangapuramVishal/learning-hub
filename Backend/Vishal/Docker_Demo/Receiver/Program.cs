using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System.Text;

namespace Receiver
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

                var consumer = new EventingBasicConsumer(channel);
                consumer.Received += (model, ea) =>
                {
                    var body = ea.Body.ToArray();
                    var message = Encoding.UTF8.GetString(body);
                    Console.WriteLine($"[x] Received: {message}");
                };

                channel.BasicConsume(queue: "hello",
                                     autoAck: true,
                                     consumer: consumer);

                Console.WriteLine("Waiting for messages...");
                Console.ReadLine();
            }
        }
    }
}
