namespace DecoratorDesignPattern
{
    /// <summary>
    /// A 'ConcreteDecorator' class for adding caramel to coffee.
    /// </summary>
    public class CaramelDecorator : CoffeeDecorator
    {
        public CaramelDecorator(ICoffee coffee) : base(coffee) { }

        public override string GetDescription() => _coffee.GetDescription() + ", Caramel";

        public override double GetCost() => _coffee.GetCost() + 2.00;
    }
}
