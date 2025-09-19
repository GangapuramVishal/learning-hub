namespace FacadeDesignPattern
{
    /// <summary>
    /// Manages inventory-related operations.
    /// </summary>
    public class InventoryService
    {
        /// <summary>
        /// Checks if the product is in stock.
        /// </summary>
        public bool CheckStock(string productId)
        {
            Console.WriteLine($"InventoryService: Checking stock for Product ID: {productId}...");
            return true;
        }

        /// <summary>
        /// Reduces the stock of the product after an order is placed.
        /// </summary>
        public void ReduceStock(string productId)
        {
            Console.WriteLine($"InventoryService: Reducing stock for Product ID: {productId}...");
        }
    }
}
