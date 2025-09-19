using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Error_Exception
{
    public class Exception
    {
        public static void Main(string[] args)
        {
            try
            {
                int result = DivideWithExceptionHandling(10, 0);
                Console.WriteLine("Result of division: " + result); // This line will not be executed.
            }
            catch (DivideByZeroException ex)
            {
                Console.WriteLine("Error: Division by zero.");
                Console.WriteLine("Exception Details: " + ex.Message);
            }
        }

        public static int DivideWithExceptionHandling(int dividend, int divisor)
        {
            if (divisor == 0)
            {
                // Exception handling: Throw a DivideByZeroException.
                throw new DivideByZeroException("Cannot divide by zero.");
            }
            else
            {
                return dividend / divisor;
            }
        }
    }
}

//In the Main method of the Exception class, the division is wrapped in a try-catch block.
//If a DivideByZeroException occurs during the division, it's caught by the catch block, and an error message is printed.