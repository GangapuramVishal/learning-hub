namespace Error_Exception
{
    public class Error
    {
        public static void Main(string[] args)
        {
            int result = DivideWithErrorHandling(10, 0);

            if (result == -1)
            {
                Console.WriteLine("Error: Division by zero.");
            }
            else
            {
                Console.WriteLine("Result of division: " + result);
            }
        }

        public static int DivideWithErrorHandling(int dividend, int divisor)
        {
            if (divisor == 0)
            {
                // Error handling: Return a special value (-1) to indicate error.
                return -1;
            }
            else
            {
                return dividend / divisor;
            }
        }
    }
}


//Error handling deals with managing errors or exceptional conditions using return values, status flags, or error codes,
//and it's typically part of the regular program flow.