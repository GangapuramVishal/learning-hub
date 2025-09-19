namespace DecoratorDesignPattern
{
    /// <summary>
    /// The 'Decorator' abstract class that implements the ICoffee interface and has a reference to an ICoffee object.
    /// </summary>
    public abstract class CoffeeDecorator : ICoffee
    {
        protected ICoffee _coffee;

        public CoffeeDecorator(ICoffee coffee)
        {
            _coffee = coffee;
        }

        public virtual string GetDescription() => _coffee.GetDescription();

        public virtual double GetCost() => _coffee.GetCost();
    }
}
