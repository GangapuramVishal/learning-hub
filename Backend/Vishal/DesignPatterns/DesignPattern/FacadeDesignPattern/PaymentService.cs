namespace FacadeDesignPattern
{
    public class PaymentService
    {
        /// <summary>
        /// Handles payment-related operations.
        /// </summary>
        public bool ProcessPayment(string paymentDetails)
        {
            Console.WriteLine("PaymentService: Processing payment...");
            return true;
        }
    }
}
