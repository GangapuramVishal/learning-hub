namespace FactroyMethod
{
    /// <summary>
    /// A 'ConcreteProduct' class for Bike that inherits from Vehicle.
    /// </summary>
    public class Bike : Vehicle
    {
        public override void Drive()
        {
            Console.WriteLine("Riding a bike!");
        }
    }
}
