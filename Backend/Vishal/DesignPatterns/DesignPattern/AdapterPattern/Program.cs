namespace AdapterPattern
{
    public class Program
    {
        static void Main(string[] args)
        {
            // Using the legacy system directly
            LegacyPaymentSystem legacySystem = new LegacyPaymentSystem();

            // Using the adapter to make the legacy system compatible with the client
            IPaymentProcessor adapter = new PaymentAdapter(legacySystem);

            // Client code works with the adapter as if it's the new system
            PaymentClient client = new PaymentClient(adapter);
            client.ProcessPayment();

            Console.ReadLine();
        }
    }
}
