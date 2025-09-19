namespace DecoratorDesignPattern
{
    /// <summary>
    /// The 'Component' interface that defines the operations.
    /// </summary>
    public interface ICoffee
    {
        string GetDescription();

        double GetCost();
    }
}
