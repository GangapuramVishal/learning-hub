using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SingleResponsibilityPrinciple
{
    public class SRP
    {
        /*The Single Responsibility Principle (SRP) states that a class should have only one
         * reason to change, meaning it should have only one responsibility. In other words,
         * a class should have only one job or function within the software system.
         * 
         *If there is a change in logging requirement then a class implementing logging functionality
         *can undergo a change but that same class should never undergo a change for change in any other functionality other than logging.
         */
    }

    //Code Violation of SRP:
    //public class OrderService
    //{
    //    public void CreateOrder(String OrderDetails)
    //    {
    //        string OrderId = OrderDetails;
    //        Console.WriteLine(OrderId);
    //    }

    //    public void ShippingDetails()
    //    {
    //        Console.WriteLine("Details about shipping");
    //    }
    //    public void CalculateBill()
    //    {
    //        Console.WriteLine("To calculate total bill");
    //    }

    //    public void MakePayment(string OrderID)
    //    {
    //    Console.WriteLine("Payment done");
    //    }
    //} 


    // Code Following SRP

    public class OrderService
    {
        public void CreateOrder(String OrderDetails)
        {
            string OrderId = OrderDetails;
            Console.WriteLine(OrderId);


        }
    }

    public class ShippingDetails
    {
        public void TrackOrder()
        {
            Console.WriteLine("Your order is on the way");
        }
    }

    public class CalculateBill
    {
        public void TotalBill()
        {
            Console.WriteLine("Total Amount");
        }
    }

    public class PaymentDetails
    {
        public void MakePayment(string OrderID)
        {
            Console.WriteLine("Payment Done");
        }
    }

}
