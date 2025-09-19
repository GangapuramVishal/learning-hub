//using System;
//using System.Collections;
//using System.Collections.Generic;


//namespace NonGenerics
//{

//    /*
//     * The Non-Generic Queue Collection Class in C# works in the FIFO(First-In-First-Out) principle.
//     * That means the item which is added first will be removed first from the collection.
//     * 
//     * Enqueue() method, we can add elements at the end of the queue.
//     * The Dequeue() method, will remove and return the first element from the queue.
//     * The queue Peek() method, will always return the first element of the queue, and it won’t delete elements from the queue.
//     * The Non-Generic Queue Collection allows both null and duplicate values.
//     */
//    public class MyQueue
//    {
//        public static void Main()
//        {
//            Queue MyQueue = new Queue();

//            //enqueue items on the MyQueue
//            Console.WriteLine("Enqueuing items onto the MyQueue: ");
//            MyQueue.Enqueue(1);
//            MyQueue.Enqueue("Two");
//            MyQueue.Enqueue(3);
//            MyQueue.Enqueue("Four");
//            MyQueue.Enqueue(5);

//            DisplayMyQueue(MyQueue);

//            //peek at the first item
//            Console.WriteLine("\nPeeking at the first item:");
//            Console.WriteLine("First item in the queue: " + MyQueue.Peek());
//            DisplayMyQueue(MyQueue);

//            //Dequeue items from the queue
//            Console.WriteLine("\nDequed item: " + MyQueue.Dequeue());
//            DisplayMyQueue(MyQueue);

//            //clear the queue
//            Console.WriteLine("\nclearing the Queue");
//            MyQueue.Clear();

//            Console.WriteLine("\nIs the queue empty? " + (MyQueue.Count == 0));
//        }

//        public static void DisplayMyQueue(Queue MyQueue)
//        {
//            Console.WriteLine("\nItems in Queue");
//            foreach(var item in MyQueue)
//            {
//                Console.WriteLine(item);
//            }
//        }
//    }
//}
