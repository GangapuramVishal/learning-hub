using System;
using System.Threading;

namespace MultiThreading
{
    public class Program
    {

        public static void Test1()
        {
            for(int i = 1; i <= 10; i++)
            {
                Console.WriteLine("Test1: " + i);
            }
            Console.WriteLine("Threading 1 is exiting.");
        }
        public static void Test2()
        {
            for (int i = 1; i <= 10; i++)
            {
                Console.WriteLine("Test2: " + i);
                if(i == 3)
                {
                    Console.WriteLine("Thread2 is going to sleep");
                    Thread.Sleep(5000);
                    Console.WriteLine("Thread2 woke up");
                }
            }
            Console.WriteLine("Threading 2 is exiting.");
        }
        public static void Test3()
        {
            for (int i = 1; i <= 10; i++)
            {
                Console.WriteLine("Test3: " + i);
                if (i == 5)
                {
                    Console.WriteLine("Thread3 is going to sleep");
                    Thread.Sleep(3000);
                    Console.WriteLine("Thread3 woke up");
                }
            }
            Console.WriteLine("Threading 3 is exiting.");
        }

        static void Main(string[] args)
        {
            Thread T1 = new Thread(Test1);
            Thread T2 = new Thread(Test2);
            Thread T3 = new Thread(Test3);
            T1.Start();
            T2.Start();
            T3.Start();
            Console.WriteLine("Thread Main is exiting");
        }
    }
}
