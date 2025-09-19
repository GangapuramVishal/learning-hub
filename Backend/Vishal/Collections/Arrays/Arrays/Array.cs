using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Arrays
{
    /*The array is defined as a collection of similar data elements. If you have some sets of integers,
    * and some sets of floats, you can group them under one name as an array. So, in simple words,
    * we can define an array as a collection of similar types of values that are stored in a contiguous memory location under a single name.
    * 
    * Single dimensional array: the data is arranged in the form of a row 
    * Multi-dimensional array:  the data is arranged in the form of rows and columns
    * Jagged array: Whose rows and columns are not equal
    * Rectangular array: Whose rows and columns are equal
    */

    public class Array
    {
        public static int[] GetArrayFromUSer()
        {
            Console.WriteLine("Enter the size of array: ");
            int size = Convert.ToInt32(Console.ReadLine());

            int[] numbers = new int[size];

            for (int i = 0; i < size; i++)
            {
                Console.WriteLine($"Enter value for element {i + 1}: ");
                numbers[i] = int.Parse(Console.ReadLine());
            }
            return numbers;
        }
        public static void DisplayArray(int[] array)
        {
            Console.Write("\nArray elements: ");
            foreach (int number in array)
            {
                Console.Write(number + " ");
            }
        }

    }
}
