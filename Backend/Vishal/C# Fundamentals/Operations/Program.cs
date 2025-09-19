namespace Operations
{
    internal class Program
    {
        static void Main(string[] args)
        {

            ArrayNumAddition arrayNumAddition = new();
            arrayNumAddition.ArrayAddition();

            arthamaticOperations operation = new();
            operation.PerformOperations();

            Circle circle = new Circle();
            circle.AreaPerimeter();


        }
    }
}
