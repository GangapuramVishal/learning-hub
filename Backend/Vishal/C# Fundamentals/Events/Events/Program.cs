using System;

namespace Events
{

    // Define event args to pass stock price information
    public class StockPriceChangedEventArgs : EventArgs
    {
        public double NewPrice { get; }

        public StockPriceChangedEventArgs(double newPrice)
        {
            NewPrice = newPrice;
        }
    }

    // Define a delegate type for handling stock price changes
    public delegate void StockPriceChangedEventHandler(object sender, StockPriceChangedEventArgs e);

    // The Stock class represents a stock and has a PriceChanged event that notifies subscribers when the stock price changes.
    public class Stock
    {
        private double _price;

        // Event for notifying when the stock price changes
        public event StockPriceChangedEventHandler PriceChanged;

        public double Price
        {
            get { return _price; }
            set
            {
                if (_price != value)
                {
                    _price = value;
                    // Raise the PriceChanged event when the price changes
                    OnPriceChanged(new StockPriceChangedEventArgs(value));
                }
            }
        }
        //Protected Method for Raising Event
        protected virtual void OnPriceChanged(StockPriceChangedEventArgs e)
        {
            // Check if any methods are subscribed to the PriceChanged event
            PriceChanged?.Invoke(this, e);    //this referes to "stock" 
        }
    }

    // The StockMonitor class represents a listener/subscriber to stock price changes. HandlePriceChanged method
    // is the event handler that gets executed when the stock price changes.
    public class StockMonitor
    {
        // Method to handle stock price changes
        public void HandlePriceChanged(object sender, StockPriceChangedEventArgs e)      
        {
            Console.WriteLine($"New stock price: ${e.NewPrice}");
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            // Create a new stock instance
            Stock microsoftStock = new Stock();

            // Create a stock monitor instance
            StockMonitor monitor = new StockMonitor();

            // Subscribe the monitor's method to the stock's PriceChanged event
            microsoftStock.PriceChanged += monitor.HandlePriceChanged;

            // Simulate stock price changes
            microsoftStock.Price = 150.25; // Output: "New stock price: $150.25"
            microsoftStock.Price = 155.50; // Output: "New stock price: $155.50"
        }
    }

}
