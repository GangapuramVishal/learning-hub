namespace FactroyMethod
{
    /// <summary>
    /// A 'ConcreteProduct' class for Car that inherits from Vehicle.
    /// </summary>
    public class Car : Vehicle
    {
        public override void Drive()
        {
            Console.WriteLine("Driving a car!");
        }
    }
}
