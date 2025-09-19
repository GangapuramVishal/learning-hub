//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Text;
//using System.Threading.Tasks;

//namespace Generics
//{
//    /*a generic Stack<T> represents a last-in, first-out (LIFO) collection of objects. It operates like a stack of plates,
//     * where you can only add or remove items from the top of the stack. The last item added to the stack is the first one to be removed.
//     * example: Undo functionalities 
//     */
//    internal class MyStack
//    {
//        static void Main()
//        {
//            // Creating a generic stack of integers
//            Stack<int> numbersStack = new Stack<int>();

//            // Adding elements to the stack
//            numbersStack.Push(10);
//            numbersStack.Push(20);
//            numbersStack.Push(30);

//            // Displaying all elements in the stack
//            Console.WriteLine("Elements in the stack:");
//            foreach (int num in numbersStack)
//            {
//                Console.WriteLine(num);
//            }

//            // Removing an element from the stack
//            int removedElement = numbersStack.Pop();
//            Console.WriteLine($"\nRemoved element from the top of the stack: {removedElement}");

//            // Displaying the remaining elements in the stack
//            Console.WriteLine("\nElements in the stack after pop:");
//            foreach (int num in numbersStack)
//            {
//                Console.WriteLine(num);
//            }

//            // Checking if a specific element exists in the stack
//            int searchElement = 20;
//            Console.WriteLine($"\nIs {searchElement} present in the stack? {numbersStack.Contains(searchElement)}");

//            // Getting the number of elements in the stack
//            Console.WriteLine($"\nNumber of elements in the stack: {numbersStack.Count}");

//            // Clearing the stack
//            numbersStack.Clear();
//            Console.WriteLine("\nStack cleared. Number of elements in the stack: {0}", numbersStack.Count);

//            Console.ReadKey();
//        }
//    }
//}


















///*Here are some key characteristics of Stack<T>:

//LIFO Behavior: Items are added to the top of the stack using the Push() method and removed from the top of the stack using the Pop() method.

//Adding and Removing Elements: In addition to Push() and Pop(), you can also inspect the element at the top of the stack without removing it using the Peek() method.

//Counting Elements: You can get the number of elements in the stack using the Count property.

//Clearing the Stack: You can remove all elements from the stack using the Clear() method.

//Checking for Elements: You can check whether an element exists in the stack using the Contains() method.

//Converting to Array: You can convert the elements of the stack to an array using the ToArray() method.
//*/