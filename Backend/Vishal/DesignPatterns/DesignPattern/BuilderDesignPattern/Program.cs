namespace BuilderDesignPattern
{
    /// <summary>
    /// Demonstrates the Builder pattern by constructing a specific type of car (SUV).
    /// </summary>
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("SUV Car....\n");
            CarBuilder builder = new SUVCarBuilder();

            CarDirector director = new CarDirector(builder);

            director.ConstructCar();

            Car car = builder.GetCar();

            car.ShowSpecifications();

            Console.WriteLine();

            //Benze car
            Console.WriteLine("Benze Car....\n");
            CarBuilder benzeBuilder = new BenzeCarBuilder();

            CarDirector benzeDirector = new CarDirector(benzeBuilder);

            benzeDirector.ConstructCar();

            Car car2 = benzeBuilder.GetCar();

            car2.ShowSpecifications();  

        }
    }
}
