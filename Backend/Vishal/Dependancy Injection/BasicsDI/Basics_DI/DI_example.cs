using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Basics_DI
{
    public class DI_example
    {
        /*What does the Inversion of control principle say?
         * 
         * Don't Call Me; we will call you.
         * 
         * In other words, the Main class should not have a concrete implementation of an aggregated class, rather,
         * it should depend on the abstraction of that class. The College class should depend on TechEvents class
         * abstraction using an interface or abstract class
         * 
         * The Dependency Injection Design Pattern in C# allows us to develop Loosely Coupled Software Components.
         * In other words, we can say that Dependency Injection Design Pattern is used to reduce the Tight Coupling
         * between the Software Components. As a result, we can easily manage future changes and other complexities
         * in our application. In this case, if we change one component, then it will not impact the other components.
         * Tight Coupling: means two objects are dependent on each other directly.
         * Loosely Coupling: means two objects are dependent on each other but using an 3rd party class => interface, abstraction.
         * 
         * Always remember dependencies are injected into dependent classes.This is typically done through constructor injection, property injection, or method injection.
         * 
         * The Dependency Injection Design Pattern in C# is a process in which we are injecting the dependent object of
         * a class into a class that depends on that object. The Dependency Injection Design Pattern is the most
         * commonly used design pattern nowadays to remove the dependencies between the objects.
         */

        public interface IMessageService
        {
            public void SendMessage(string recipient, string message);  // which has no implementation
        }

        public class EmailService :IMessageService   //dependency class 
        {
            public void SendMessage(string recipient, string message)
            {
                Console.WriteLine($"Sending email to {recipient}: {message}");
            }
        }

        public class SMSService : IMessageService   //dependency class 
        {
            public void SendMessage(string recipient, string message)
            {
                Console.WriteLine($"Sending SMS to {recipient}: {message}");
            }
        }
        public class MessageSender    //Dependent class
        {
            private readonly IMessageService _messageService;
            public MessageSender(IMessageService messageService)
            {
                _messageService = messageService;
            }
            public void SendMessage(string recipient, string message)
            {
                _messageService.SendMessage(recipient, message);
            }
        }
    }
}
