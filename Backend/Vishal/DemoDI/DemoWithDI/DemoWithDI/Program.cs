namespace DemoWithDI
{
    // Engine interface representing a car engine
    public interface IEngine
    {
        void Start();
        void Stop();
    }
    // GasolineEngine class implementing the IEngine interface
    public class GasolineEngine : IEngine
    {
        public void Start()
        {
            Console.WriteLine("Gasoline engine started");
        }

        public void Stop()
        {
            Console.WriteLine("Gasoline engine stopped");
        }
    }
    // ElectricEngine class implementing the IEngine interface
    public class ElectricEngine : IEngine
    {
        public void Start()
        {
            Console.WriteLine("Electric engine started");
        }

        public void Stop()
        {
            Console.WriteLine("Electric engine stopped");
        }
    }
    // Car class representing a car that accepts an engine via constructor (DI)
    public class Car
    {
        private readonly IEngine engine;

        public Car(IEngine engine)
        {
            this.engine = engine;
        }

        public void Start()
        {
            engine.Start();
        }

        public void Stop()
        {
            engine.Stop();
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            // Create an instance of the ElectricEngine class
            IEngine electricEngine = new ElectricEngine();
            IEngine gasolineEngine = new GasolineEngine();

            // Pass the electric engine instance to the Car constructor
            Car car = new Car(gasolineEngine);

            // Start and stop the car
            car.Start();
            car.Stop();
        }
    }
}
