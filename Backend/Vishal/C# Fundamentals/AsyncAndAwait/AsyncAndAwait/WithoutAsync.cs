using System;
using System.Threading.Tasks;

namespace AsyncAndAwait
{
    public class WithoutAsync
    {
        static void Main(string[] args)
        {
            Method1();
            Method2();
            Console.ReadKey();
        }

        public static void Method1()
        {
            Task.Run(() =>
            {
                for (int i = 0; i < 10; i++)
                {
                    Console.WriteLine("Method1 is running");
                    Task.Delay(100).Wait();
                }
            }).Wait();
        }

        public static void Method2()
        {
            for (int i = 0; i < 10; i++)
            {
                Console.WriteLine("Method2 is running");
                Task.Delay(100).Wait();
            }
        }
    }
}