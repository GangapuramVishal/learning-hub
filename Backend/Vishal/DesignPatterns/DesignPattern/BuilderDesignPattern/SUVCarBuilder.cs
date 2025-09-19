namespace BuilderDesignPattern
{
    /// <summary>
    /// The 'ConcreteBuilder' class for creating a specific type of Car (SUV).
    /// </summary>
    public class SUVCarBuilder : CarBuilder
    {
        public override void BuildEngine()
        {
            car.Engine = "V6 Engine";
        }
        public override void BuildWheels()
        {
            car.Wheels = "18-inch Alloy Wheels";
        }
        public override void BuildBody()
        {
            car.Body = "SUV Body";
        }
        public override void AddGPS()
        {
            car.HasGPS = true;
        }

        public override void AddSunroof()
        {
            car.HasSunroof = true;
        }
    }
}
