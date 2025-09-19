namespace AdapterPattern
{
    /// <summary>
    /// The 'ITarget' interface defines the domain-specific interface used by the client.
    /// </summary>
    public interface IPaymentProcessor
    {
        /// <summary>
        /// Method to process payments, as expected by the client.
        /// </summary>
        void MakePayment();

    }
}
