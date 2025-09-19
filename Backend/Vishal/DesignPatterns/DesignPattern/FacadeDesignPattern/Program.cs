namespace FacadeDesignPattern
{
    internal class Program
    {
        /// <summary>
        /// The client code that interacts with the OrderFacade to process an order.
        /// </summary>
        static void Main(string[] args)
        {
            Console.WriteLine("E-Commerce Order Processing System:");

            var orderFacade = new OrderFacade();

            string productId = "P12345";
            string paymentDetails = "Card: ****-****-****-1234";
            string address = "123 Main St, Springfield";
            string email = "customer@example.com";

            bool orderSuccess = orderFacade.ProcessOrder(productId, paymentDetails, address, email);

            Console.WriteLine(orderSuccess
                ? "Order completed successfully!"
                : "Order processing failed.");
        }
    }
}
