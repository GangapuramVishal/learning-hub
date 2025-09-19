namespace DecoratorDesignPattern
{
    /// <summary>
    /// Demonstrates the use of the Decorator Pattern in a coffee ordering scenario.
    /// </summary>
    public class Program
    {
        public static void Main()
        {
            ICoffee coffee = new BasicCoffee();
            Console.WriteLine($"{coffee.GetDescription()} : ${coffee.GetCost()}");

            coffee = new MilkDecorator(coffee);
            Console.WriteLine($"{coffee.GetDescription()} : ${coffee.GetCost()}");

            coffee = new SugarDecorator(coffee);
            Console.WriteLine($"{coffee.GetDescription()} : ${coffee.GetCost()}");

            coffee = new CaramelDecorator(coffee);
            Console.WriteLine($"{coffee.GetDescription()} : ${coffee.GetCost()}");
        }
    }
}
