using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Basic_Programs
{
    internal class MethodOverLoading
    {
        public void AddNumbers(int a, int b)
        {
            Console.WriteLine(a + b);
        }

        public void AddNumbers(int a, int b, int c)
        {
            Console.WriteLine(a + b + c);
        }
    }
}
