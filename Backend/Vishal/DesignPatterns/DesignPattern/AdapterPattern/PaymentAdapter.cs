namespace AdapterPattern
{
    /// <summary>
    /// The 'Adapter' class makes the Adaptee's interface compatible with the ITarget interface.
    /// </summary>
    public class PaymentAdapter : IPaymentProcessor
    {
        private readonly LegacyPaymentSystem _legacyPaymentSystem;

        /// <summary>
        /// Constructor that initializes the adapter with the Adaptee.
        /// </summary>
        /// <param name="legacyPaymentSystem">Instance of the legacy system to adapt.</param>
        public PaymentAdapter(LegacyPaymentSystem legacyPaymentSystem)
        {
            _legacyPaymentSystem = legacyPaymentSystem;
        }

        /// <summary>
        /// Method to adapt the client's MakePayment request to the legacy system's ProcessPayment method.
        /// </summary>
        public void MakePayment()
        {
            Console.WriteLine("Adapting payment request...");
            _legacyPaymentSystem.ProcessPayment();
        }
    }
}
