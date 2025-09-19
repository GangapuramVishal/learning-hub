using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DelegatesForPublishSubscriberDesignPattern
{
    public class SMSService
    {
        public void SendSMS()   //event handler method
        {
            Console.WriteLine("Sending SMS.....");
        }
    }
}
