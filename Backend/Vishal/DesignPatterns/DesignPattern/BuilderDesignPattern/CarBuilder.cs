namespace BuilderDesignPattern
{
    /// <summary>
    /// The 'Builder' abstract class that defines methods for creating parts of the Product (Car).
    /// </summary>
    public abstract class CarBuilder
    {
        protected Car car = new Car();

        /// <summary>
        /// Initializes a new instance of the Car class.
        /// </summary>
        public Car GetCar()
        {
            return car; 
        }

        /// <summary>
        /// Abstract method to build the car engine.
        /// </summary>
        public abstract void BuildEngine();

        /// <summary>
        /// Abstract method to build the car wheels.
        /// </summary>
        public abstract void BuildWheels();

        /// <summary>
        /// Abstract method to build the car body.
        /// </summary>
        public abstract void BuildBody();

        /// <summary>
        /// Optional feature for GPS.
        /// </summary>
        public abstract void AddGPS();

        /// <summary>
        /// Optional feature for Sunroof.
        /// </summary>
        public abstract void AddSunroof();
    }
}
