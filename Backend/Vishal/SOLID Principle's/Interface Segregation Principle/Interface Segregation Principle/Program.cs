namespace Interface_Segregation_Principle
{
    internal class Program
    {
        static void Main(string[] args)
        {
           //===== Following ISP=================
           Car car = new Car();
            Console.WriteLine("Using Car");
            car.Start();
            car.Drive();

            FlyingCar flyingCar = new FlyingCar();
            Console.WriteLine("Using FlyingCar");
            flyingCar.Start();
            flyingCar.Drive();
            flyingCar.Fly();
        }
    }
}
