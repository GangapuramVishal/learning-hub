using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Operations
{
    public class ArrayNumAddition
    {
        public void ArrayAddition()
        {
            var numbers = new[] { 10, 12, 13, 14, 15, 16, 17, 18, 19, 20 };
            int sum = 0;
            foreach(var number in numbers)
            {
                sum += number;
            }
            Console.WriteLine($"The sum of array num {sum}");
        }
    }
}
