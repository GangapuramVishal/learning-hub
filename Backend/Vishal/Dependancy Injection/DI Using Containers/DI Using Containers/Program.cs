using Microsoft.Extensions.DependencyInjection;

namespace DI_Using_Containers
{
    public class Program
    {
        static void Main(string[] args)
        {
            //object is ceated for dependencies are registered for dependency injection.
            var services = new ServiceCollection();

            //new instance of the specified type will be created every time it is requested from the service provider.
            services.AddTransient<IAreaCalculator, AreaCalculator>();
            services.AddTransient<Rectangle>();

            services.AddTransient<IPerimeterCalculator, PerimeterCalculator>();
            services.AddTransient<Rectangle>();

            // Build the service provider
            var serviceProvider = services.BuildServiceProvider(); //After registering all dependencies, the BuildServiceProvider method is called to create a service provider.

            // Resolve and use the rectangle

            /*The GetRequiredService<T>() method is a part of the .NET Core Dependency Injection (DI) framework
             * provided by the Microsoft.Extensions.DependencyInjection namespace. This method is used to retrieve 
             * an instance of a service registered with the DI container.
             */
            var rectangle = serviceProvider.GetRequiredService<Rectangle>(); 
            double area = rectangle.GetArea(5, 3);
            Console.WriteLine($"Area of rectangle: {area}");

            var rectangleP = serviceProvider.GetRequiredService<Rectangle>();
            double perimeter = rectangleP.GetPerimeter(5, 3);
            Console.WriteLine($"Perimeter of rectangle: {perimeter}");


        }

    }
}
