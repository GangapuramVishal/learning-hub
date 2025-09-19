using System.Runtime.Intrinsics.X86;
using static Basics_DI.DI_example;

namespace Basics_DI
{
    public class Program
    {
        static void Main(string[] args)
        {
            //---------------DI_example-----------------------
            //IMessageService emailService = new EmailService();
            //IMessageService smsService = new SMSService();

            //MessageSender emailMessageSender = new MessageSender(emailService);
            //MessageSender smsMessageSender = new MessageSender(smsService);

            //emailMessageSender.SendMessage("gangapuramvishal@gmail.com", "This message is sent from Email using DI");
            //smsMessageSender.SendMessage("8520861477", "This message is sent from number using DI");


            /*   -------------- Constructor Injection -----------------

            IEvent sportsEvent = new SportsEvent();
            IEvent culturalEvent = new CulturalEvent();
            IEvent techEvent = new TechEvent();
            IEvent partyEvent = new PartyEvent();


            // Create a College object and inject different types of events
            College college = new College(sportsEvent);

            // Call the GetEvents method to load event details
            Console.WriteLine("College is hosting the following events:");
            college.GetEvents();

            // Change the injected event to a cultural event
            college = new College(culturalEvent);
            college.GetEvents();

            // Change the injected event to a tech event
            college = new College(techEvent);
            college.GetEvents();

            // Change the injected event to a party event
            college = new College(partyEvent);
            college.GetEvents();

            Console.ReadLine(); // Keep console window open

            */

            //-------------- PropertyInjection_Di-----------------------

            Service1 s1 = new Service1();
            Service2 s2 = new Service2();

            Client client = new Client();

            client.Service = s2;

            client.ServeMethod();


            //------------------- MethodInjection_DI---------------------

            ////Create instances of operations
            //var addition = new AdditionOperation();
            //var subtraction = new SubstractionOperation();

            //// Create an instance of the Calculator
            //var calculator = new Calculator();

            //// Use method injection to perform addition
            //int result = calculator.Calculate(5, 3, addition);
            //Console.WriteLine($"Result of addition: {result}");

            //// Use method injection to perform subtraction
            //result = calculator.Calculate(5, 3, subtraction);
            //Console.WriteLine($"Result of subtraction: {result}");
        }
    }
}
