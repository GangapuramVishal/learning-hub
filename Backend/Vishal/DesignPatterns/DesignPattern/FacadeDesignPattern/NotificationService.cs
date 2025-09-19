namespace FacadeDesignPattern
{
    public class NotificationService
    {
        /// <summary>
        /// Manages order confirmation notifications.
        /// </summary>
        public void SendOrderConfirmation(string email)
        {
            Console.WriteLine($"NotificationService: Sending order confirmation email to {email}...");
        }
    }
}
