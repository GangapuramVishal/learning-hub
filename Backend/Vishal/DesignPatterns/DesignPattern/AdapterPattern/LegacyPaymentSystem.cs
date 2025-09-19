namespace AdapterPattern
{
    /// <summary>
    /// The 'Adaptee' class contains the existing functionality that needs to be adapted.
    /// </summary>
    public class LegacyPaymentSystem
    {
        /// <summary>
        /// Existing method in the legacy payment system that we want to adapt.
        /// </summary>
        public void ProcessPayment()
        {
            Console.WriteLine("Processing payment using the legacy payment system.");
        }
    }
}
