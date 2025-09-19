using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DelegatesPOC
{
    //defining a delegate
    public delegate void AddDelegate(int x, int y);
    public delegate string SayDelegate(string str);
    public class WithDelegates
    {
        public void AddNumbers(int a, int b)
        {
            Console.WriteLine(a + b);
        }

        //static method
        public static string SayHello(string name)
        {
            return "Hello" + name;
        }
    }
}
