//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Text;
//using System.Threading.Tasks;

//namespace Generics
//{
//    /*In C#, a generic Queue<T> represents a first-in, first-out (FIFO) collection of objects. It allows you to store elements 
//     * in a sequence where the first element added is the first one to be removed. Think of it as a line in a queue where the 
//     * first person to enter the line is the first person to leave.
//     * 
//     *Example: Recyclebin deleting operation 
//     */
//    public class MyQueue
//    {
//        static void Main()
//        {
//            // Creating a generic queue of integers
//            Queue<int> numbersQueue = new Queue<int>();

//            // Adding elements to the queue
//            numbersQueue.Enqueue(10);
//            numbersQueue.Enqueue(20);
//            numbersQueue.Enqueue(30);

//            // Displaying all elements in the queue
//            Console.WriteLine("Elements in the queue:");
//            foreach (int num in numbersQueue)
//            {
//                Console.WriteLine(num);
//            }

//            // Removing an element from the queue
//            int removedElement = numbersQueue.Dequeue();
//            Console.WriteLine($"\nRemoved element from the queue: {removedElement}");

//            // Displaying the remaining elements in the queue
//            Console.WriteLine("\nElements in the queue after dequeue:");
//            foreach (int num in numbersQueue)
//            {
//                Console.WriteLine(num);
//            }

//            // Checking if a specific element exists in the queue
//            int searchElement = 20;
//            Console.WriteLine($"\nIs {searchElement} present in the queue? {numbersQueue.Contains(searchElement)}");

//            // Getting the number of elements in the queue
//            Console.WriteLine($"\nNumber of elements in the queue: {numbersQueue.Count}");

//            // Clearing the queue
//            numbersQueue.Clear();
//            Console.WriteLine($"\nQueue cleared. Number of elements in the queue: {numbersQueue.Count}");

//            Console.ReadKey();
//        }
//    }
//}
