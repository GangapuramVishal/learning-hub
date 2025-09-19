namespace BuilderDesignPattern
{
    /// <summary>
    /// The 'Director' class that manages the construction process.
    /// </summary>
    public class CarDirector
    {
        private CarBuilder builder;

        /// <summary>
        /// Constructor for the Director which takes a CarBuilder object.
        /// </summary>
        public CarDirector(CarBuilder builder)
        {
            this.builder = builder;
        }

        /// <summary>
        /// Directs the builder to construct the car step by step.
        /// </summary>
        public void ConstructCar()
        {
            builder.BuildEngine();
            builder.BuildWheels();
            builder.BuildBody();
            builder.AddGPS();
            builder.AddSunroof();
        }
    }
}
