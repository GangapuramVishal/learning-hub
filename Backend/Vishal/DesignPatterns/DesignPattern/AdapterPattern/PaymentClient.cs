namespace AdapterPattern
{
    /// <summary>
    /// Client class that interacts with the system via the ITarget interface.
    /// </summary>
    public class PaymentClient
    {
        private readonly IPaymentProcessor _paymentProcessor;

        /// <summary>
        /// Constructor that initializes the client with an ITarget implementation.
        /// </summary>
        /// <param name="paymentProcessor">An object that implements the ITarget interface.</param>
        public PaymentClient(IPaymentProcessor paymentProcessor)
        {
            _paymentProcessor = paymentProcessor;
        }

        /// <summary>
        /// Executes the payment processing, interacting with the system via the ITarget interface.
        /// </summary>
        public void ProcessPayment()
        {
            Console.WriteLine("Client requests payment processing...");
            _paymentProcessor.MakePayment();
        }
    }
}
