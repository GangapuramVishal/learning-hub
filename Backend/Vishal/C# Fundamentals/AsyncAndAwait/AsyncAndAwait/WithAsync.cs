//namespace AsyncAndAwait
//{
//    /* Asynchronous programming in C# .NET allows you to perform tasks concurrently without blocking the main thread.
//     * This is particularly useful for operations that may take some time to complete, such as fetching data from a
//     * web service or reading/writing files. Asynchronous programming is achieved using the async and await keywords,
//     * along with the Task class.
//     * 
//     * async: This keyword is used to define a method as asynchronous. It allows the method to use the await keyword inside it.
//     * await: This keyword is used to asynchronously wait for a task to complete. It allows the program to asynchronously wait
//     * for the result of an asynchronous operation without blocking the main thread.
//     * Task: The Task data type represents an asynchronous operation.Task and Task<T> in C# for the return data type of an asynchronous method. */

//    public class WithAsync
//    {
//        static void Main(string[] args)
//        {
//            Method1();
//            Method2();
//            Console.ReadKey();
//        }

//        public static async Task Method1()       //Task<T>
//        {
//            await Task.Run(() =>          //This code starts a new task that will run code inside the lambda expression
//            {
//                for (int i = 0; i < 10; i++)
//                {
//                    Console.WriteLine("Method1 is running");
//                    Task.Delay(100).Wait();            //This code creates a task that asynchronously delays for the specified amount of time,
//                                                       //in this case, 100 milliseconds. The Task.Delay() method returns a task that completes after the specified delay.
//                                                       //it's waiting for the delay task to finish.
//                }
//            });
//        }
//        public static void Method2()
//        {
//            for (int i = 0; i < 10; i++)
//            {
//                Console.WriteLine("Method2 is running");
//                Task.Delay(100).Wait();
//            }
//        }
//    }
//}
