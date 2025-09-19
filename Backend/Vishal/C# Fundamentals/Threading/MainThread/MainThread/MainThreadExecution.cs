using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MainThread
{
    //Drawback - Test2 should wait until Test1 Execution
    public class MainThreadExecution
    {
        static void Test1()
        {
            for(int i = 1; i <= 5; i++)
            {
                Console.WriteLine("Test1: " + i);
            }    
        }
        public static void Test2()
        {
            for(int i =1; i <= 5; i++)
            {
                Console.WriteLine("Test2: " + i);
                if (i == 3)
                {
                    Console.WriteLine("Main Thread going to sleep");
                    Thread.Sleep(7000);
                    Console.WriteLine("Main thread woke up");
                }
            }
        }
        public static void Test3()
        {
            for(int i =1;i <= 5; i++)
            {
                Console.WriteLine("Test3: " + i);
            }
        }
        public static void Main(string[] args)
        {
            Test1();
            Test2();
            Test3();
        }
    }
}
