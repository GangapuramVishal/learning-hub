using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Operations
{
    public class arthamaticOperations
    {
        public void PerformOperations()
        {
            Console.WriteLine("Enter an operator (+, -, *, /): ");
            var op = Console.ReadLine();
            Console.WriteLine();

            Console.WriteLine("Enter two numbers:");
            double number1 = Convert.ToDouble(Console.ReadLine());
            double number2 = Convert.ToDouble(Console.ReadLine());

            double result = 0;


            switch (op)
            {
                case "+":
                    result = number1 + number2;
                    break;
                case "-":
                    result = number1 - number2;
                    break;
                case "*":
                    result = number1 * number2;
                    break;
                case "/":
                    if (number2 != 0)
                        result = number1 / number2;
                    else
                        Console.WriteLine("Division by zero is not allowed.");
                    break;
                default:
                    Console.WriteLine("Invalid operator.");
                    break;
            }

            Console.WriteLine($"Result: {result}");

            


        }
    }
}
