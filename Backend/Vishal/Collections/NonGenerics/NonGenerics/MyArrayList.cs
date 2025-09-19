//using System;
//using System.Collections;
//using System.Linq;
//using System.Text;
//using System.Threading.Tasks;

//namespace NonGenerics
//{
//    /*The ArrayList in C# is a non-generic collection class that works like an array but provides the facilities such as
//    * dynamic resizing, adding, and deleting elements from the middle of a collection.  The ArrayList in C# can be used
//    * to add unknown data i.e. when we don’t know the types of data and size of the data, then we can use ArrayList. 
//    * 
//    * The Elements can be added and removed from the Array List collection at any point in time.
//    * The ArrayList is not guaranteed to be sorted.
//    * The capacity of an ArrayList is the number of elements the ArrayList can hold.
//    * Elements in this collection can be accessed using an integer index. Indexes in this collection are zero-based.
//    * It allows duplicate elements.
//    */
//    public class MyArrayList
//    {
//        public static void Main()
//        {
//            // Create an ArrayList
//            ArrayList arrayList = new ArrayList();
//            ArrayList arrayList2 = new ArrayList()
//            {
//                    "India",
//                    "USA",
//                    "UK",
//                    "Nepal"
//            };

//            // Add elements of different types to the ArrayList
//            arrayList.Add("Apple");
//            arrayList.Add(123); // Integer
//            arrayList.Add(3.14); // Double
//            arrayList.Add(true); // Boolean

//            // Display the elements of the ArrayList
//            Console.WriteLine("ArrayList elements:");
//            foreach (var item in arrayList)
//            {
//                Console.WriteLine(item);
//            }

//            // Accessing elements by index
//            Console.WriteLine("\nAccessing element at index 1: " + arrayList[1]);

//            // Inserting an element at a specific index
//            arrayList.Insert(1, "Mango");

//            // Display the modified ArrayList
//            Console.WriteLine("\nModified ArrayList elements after insertion:");
//            foreach (var item in arrayList)
//            {
//                Console.WriteLine(item);
//            }

//            // using InsertRange(int index, ICollection c)
//            arrayList.InsertRange(5, arrayList2);
//            Console.WriteLine("\nnModified Array List Elements After InsertRange");
//            foreach (var item in arrayList)
//            {
//                Console.Write($"{item} ");
//            }

//            // Removing an element
//            arrayList.Remove("Apple");

//            // Display the modified ArrayList
//            Console.WriteLine("\nModified ArrayList elements after removal:");
//            foreach (var item in arrayList)
//            {
//                Console.WriteLine(item);
//            }

//            // Clearing the ArrayList
//            arrayList.Clear();

//            // Check if the ArrayList is empty
//            Console.WriteLine("\nIs ArrayList empty? " + (arrayList.Count == 0));

//            //Creating a clone of the arrayList using Clone Method
//            ArrayList cloneArrayList = (ArrayList)arrayList2.Clone();
//            Console.WriteLine("\nCloned arrayList elements");
//            foreach (var item in cloneArrayList)
//            {
//                Console.WriteLine($"{item} ");
//            }
//            Console.ReadKey();
//        }
//    }
//}



////Advantages:
////Variable Length
////Can insert an element into the middle of the collection
////Can delete elements from the middle of the collection
////It is not type-safe, so we can store any type of data.
////Boxing and Unboxing are required as it is operated on the object data type.