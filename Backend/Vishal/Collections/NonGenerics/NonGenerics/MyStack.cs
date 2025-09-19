//using System;
//using System.Collections;
//using System.Linq;
//using System.Text;
//using System.Threading.Tasks;

//namespace NonGenerics
//{
//    public class MyStack
//    {
//        /*The Stack in C# is a Non-Generic collection class that works in the LIFO (Last in First out) principle.
//         * So, we need to use the Stack Collection Class in C#, when we want Last-In-First-Out access to the items
//         * of a collection. That means the item which is added last to the collection will be the first item to be removed from the collection. 
//         * 
//         *Operations:
//         *Push: Adds an item to the top of the stack.
//         *Pop: Removes and returns the item at the top of the stack.
//         *Peek: Returns the item at the top of the stack without removing it.
//         *Clear: Removes all items from the stack.
//         */
//        public static void Main()
//        {
//            //creating a stack object
//            Stack myStack = new Stack();

//            //pushing items into stack
//            myStack.Push(1);
//            myStack.Push("Two");
//            myStack.Push(3);
//            myStack.Push("Four");

//            //Peeking at the top Items
//            Console.WriteLine("Top Item on stack: " + myStack.Peek());

//            //poppin Items on stack
//            Console.WriteLine("\nPopped item: " + myStack.Pop());

//            Console.WriteLine("\nTop Item on stack: " + myStack.Peek());

//            //checking if the stack is empty or not

//            Console.WriteLine("\nIs Stack empty? " + (myStack.Count == 0));

//            DisplayStack(myStack);

//            //Clearing the whole stack
//            myStack.Clear();

//            Console.WriteLine("\nIs Stack emplty? " + (myStack.Count == 0));
//        }

//        static void DisplayStack(Stack myStack)
//        {
//            Console.WriteLine("\nItems in stack ");
//            foreach(var item in myStack)
//            {
//                Console.WriteLine(item);
//            }
//        }
        
//    }
//}
