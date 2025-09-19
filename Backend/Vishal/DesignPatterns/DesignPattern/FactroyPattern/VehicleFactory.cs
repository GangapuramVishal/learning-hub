namespace FactroyMethod
{
    /// <summary>
    /// The 'Creator' Factory class. This contains the logic for creating objects.
    /// The GetVehicle method is responsible for returning the right type of Vehicle based on the input.
    /// </summary>
    public class VehicleFactory
    {
        public Vehicle GetVehicle(string vehicleType)
        {
            /// <summary>
            /// The switch expression determines the concrete type of vehicle to be created.
            /// </summary>
            return vehicleType.ToLower() switch
            {
                "car" => new Car(),    
                "bike" => new Bike(),  
                "truck" => new Truck(), 
                _ => throw new ArgumentException("Invalid vehicle type.") 
            };
        }
    }
}
