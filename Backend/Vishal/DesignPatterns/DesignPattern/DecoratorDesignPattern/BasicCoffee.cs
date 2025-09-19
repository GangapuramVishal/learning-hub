namespace DecoratorDesignPattern
{
    /// <summary>
    /// The 'ConcreteComponent' class that represents a basic coffee.
    /// </summary>
    public class BasicCoffee : ICoffee
    {
        public string GetDescription() => "Basic Coffee";

        public double GetCost() => 5.00;
    }
}
