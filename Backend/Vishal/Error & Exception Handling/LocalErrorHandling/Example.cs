namespace LocalErrorHandling
{
    public class Example
    {
        public void Divide(int a, int b)
        {
            try
            {
                int result = a / b;
                Console.WriteLine($"Result of division: {result}");
            }
            catch (DivideByZeroException ex)
            {
                Console.WriteLine("Error: Cannot divide by zero.");
            }
            finally
            {
                Console.WriteLine("Finally block executed.");
            }
        }

        public void AccessArrayElement(int[] array, int index)
        {
            try
            {
                int value = array[index];
                Console.WriteLine($"Value at index {index}: {value}");
            }
            catch (IndexOutOfRangeException ex)
            {
                Console.WriteLine("Error: Index out of range.");
            }
            finally
            {
                Console.WriteLine("Finally block executed.");
            }
        }

        public void ReadFile(string filePath)
        {
            try
            {
                // Code to read file
                Console.WriteLine($"Reading file: {" "}");
                throw new System.IO.FileNotFoundException(); // Simulate file not found
            }
            catch (System.IO.FileNotFoundException ex)
            {
                Console.WriteLine("Error: File not found.");
            }
            finally
            {
                Console.WriteLine("Finally block executed.");
            }
        }
    }
}