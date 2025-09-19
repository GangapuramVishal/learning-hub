namespace DecoratorDesignPattern
{
    /// <summary>
    /// A 'ConcreteDecorator' class for adding sugar to coffee.
    /// </summary>
    public class SugarDecorator : CoffeeDecorator
    {
        public SugarDecorator(ICoffee coffee) : base(coffee) { }

        public override string GetDescription() => _coffee.GetDescription() + ", Sugar";

        public override double GetCost() => _coffee.GetCost() + 0.50;
    }
}
