using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/* This C# Program Displays All the Prime Numbers Between 1 to 100.
 Here prime number is a natural number greater than 1 that has no positive divisors other than 1 and itself.
 A prime number is greater than 1 and divided by 1 and the number it self*/


namespace Basic_Programs
{
    public class PrimeNumbers
    {
        public void PrintPrimeNumbers()
        {
            Console.WriteLine("Enter the range: ");
            int limit = int.Parse(Console.ReadLine());

            for(int i =2; i<= limit; i++)
            {
                bool isPrimeNumber = true;
                for (int j =2; j < i; j++)
                {
                    if (i % j == 0)
                    {
                        isPrimeNumber = false;
                        break;
                    }
                }
                if (isPrimeNumber)
                {
                    Console.WriteLine(i);
                }
            }
            Console.ReadLine();
        }

    }
}
