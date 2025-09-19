namespace DemoWithoutDI
{
    // Engine class representing a car engine
    public class Engine
    {
        public void Start()
        {
            Console.WriteLine("Engine started");
        }

        public void Stop()
        {
            Console.WriteLine("Engine stopped");
        }
    }
    // Car class representing a car that uses an Engine
    public class Car
    {
        private Engine engine = new Engine(); // Tightly coupling with Engine

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
            Car car = new Car();

            car.Start(); // Start the car
            car.Stop();  // Stop the car
        }
    }
}

