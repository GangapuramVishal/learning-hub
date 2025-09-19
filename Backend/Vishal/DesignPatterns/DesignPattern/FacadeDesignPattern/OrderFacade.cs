namespace FacadeDesignPattern
{
    /// <summary>
    /// A facade that simplifies the interaction with the order processing subsystems.
    /// </summary>
    public class OrderFacade
    {
        private readonly PaymentService _paymentService;
        private readonly InventoryService _inventoryService;
        private readonly ShippingService _shippingService;
        private readonly NotificationService _notificationService;

        public OrderFacade()
        {
            _paymentService = new PaymentService();
            _inventoryService = new InventoryService();
            _shippingService = new ShippingService();
            _notificationService = new NotificationService();
        }

        /// <summary>
        /// Processes an order by coordinating payment, inventory, shipping, and notification subsystems.
        /// </summary>
        public bool ProcessOrder(string productId, string paymentDetails, string address, string email)
        {
            Console.WriteLine("OrderFacade: Starting order processing...");

            if (!_inventoryService.CheckStock(productId))
            {
                Console.WriteLine("OrderFacade: Order failed. Product is out of stock.");
                return false;
            }

            if (!_paymentService.ProcessPayment(paymentDetails))
            {
                Console.WriteLine("OrderFacade: Order failed. Payment could not be processed.");
                return false;
            }

            _inventoryService.ReduceStock(productId);
            _shippingService.ArrangeShipping(address);
            _notificationService.SendOrderConfirmation(email);

            Console.WriteLine("OrderFacade: Order processed successfully.");
            return true;
        }
    }
}
