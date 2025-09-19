namespace SingleResponsibilityPrinciple
{
    internal class Program
    {
        static void Main(string[] args)
        {
            //OrderService orderService = new OrderService();

            //Code Violation of SRP:
            //orderService.CreateOrder("laptop");
            //orderService.ShippingDetails();
            //orderService.CalculateBill();
            //orderService.MakePayment("LAP123");

            //Code Following SRP

            OrderService orderService = new OrderService();
            ShippingDetails shippingDetails = new ShippingDetails();
            CalculateBill calculateBill = new CalculateBill();
            PaymentDetails paymentDetails = new PaymentDetails();

            // Create an order
            orderService.CreateOrder("Order details");

            // Track the order
            shippingDetails.TrackOrder();

            // Calculate the bill
            calculateBill.TotalBill();

            // Make payment for the order
            paymentDetails.MakePayment("123"); // Passing dummy OrderID

        }
    }

}
