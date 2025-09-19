using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DelegatesForPublishSubscriberDesignPattern
{
    public class EmailService
    {
        public void SendEmail()     //event handler method
        {
            Console.WriteLine("sending Email....");
        }
    }
}
