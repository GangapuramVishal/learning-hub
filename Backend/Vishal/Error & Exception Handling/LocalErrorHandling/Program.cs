namespace LocalErrorHandling
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Example example = new Example();

            // Example 1: Divide by zero error
            example.Divide(10, 0);

            // Example 2: Index out of range error
            int[] array = { 1, 2, 3 };
            example.AccessArrayElement(array, 5);

            // Example 3: File not found error
            example.ReadFile("example.txt");
        }
    }
}
