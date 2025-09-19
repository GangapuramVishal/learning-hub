namespace FacadeDesignPattern
{
    public class ShippingService
    {
        /// <summary>
        /// Handles shipping-related operations.
        /// </summary>
        public void ArrangeShipping(string address)
        {
            Console.WriteLine($"ShippingService: Arranging shipping to {address}...");
        }
    }
}
