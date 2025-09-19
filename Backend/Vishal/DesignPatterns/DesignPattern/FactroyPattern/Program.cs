namespace FactroyMethod
{
    /// <summary>
    /// Example of how to use the Factory pattern in a client application.
    /// </summary>
    public class Program
    {
        static void Main(string[] args)
        {
            VehicleFactory factory = new VehicleFactory();

            Vehicle myCar = factory.GetVehicle("car");
            myCar.Drive();

            Vehicle myBike = factory.GetVehicle("bike");
            myBike.Drive();

            Vehicle myTruck = factory.GetVehicle("truck");
            myTruck.Drive();

            try
            {
                Vehicle unknownVehicle = factory.GetVehicle("plane");
                unknownVehicle.Drive();
            }
            catch (ArgumentException ex)
            {
                Console.WriteLine(ex.Message);
            }
        }
    }
}
