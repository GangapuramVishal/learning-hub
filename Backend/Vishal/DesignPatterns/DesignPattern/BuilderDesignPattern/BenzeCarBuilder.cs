namespace BuilderDesignPattern
{
    public class BenzeCarBuilder : CarBuilder
    {
        public override void AddGPS()
        {
            car.HasGPS = true;
        }

        public override void AddSunroof()
        {
            car.HasSunroof = true;
        }

        public override void BuildBody()
        {
            car.Body = "Luxury Sedan Body";
        }

        public override void BuildEngine()
        {
            car.Engine = "V8 Turbo Engine";
        }

        public override void BuildWheels()
        {
            car.Wheels = "19-inch Premium Alloy Wheels";
        }
    }
}
