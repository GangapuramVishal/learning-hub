//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Text;
//using System.Threading.Tasks;

//namespace Generics
//{
//    /*The Generic HashSet<T> Collection Class in C# can be used to store, remove or view elements. It is an unordered collection of unique elements.
//     * The HashSet<T> Collection is introduced in .NET Framework 3.5. It does not allow for the addition of duplicate elements. So, it is
//     * recommended to use the HashSet collection if you want to store only unique elements. This collection is of the generic type collection
//     * 
//     * The performance of the HashSet is much better in comparison to the list collection in C#.
//     * characteristics of HashSet<T>
//     * Uniqueness, Fast Lookup, Adding and Removing Elements, Counting Elements(count property), Clearing the Set(clear()), Checking for Elements(Contains()),
//     * Set Operations - UnionWith(), IntersectWith(), ExceptWith(). 
//     * 
//     */
//    public class MyHashSet
//    {
//        public static void Main()
//        {
//            //creating two hash sets
//            HashSet<int> set1 = new HashSet<int> { 1, 2, 3, 4, 5, 6 };

//            HashSet<int> set2 = new HashSet<int> { 5, 6, 7, 8, 9, 10 };

//            //Union
//            HashSet<int> union = new HashSet<int>(set1);     //crates new hashset of union & initializes it with the elements of the existing set1
//            union.UnionWith(set2);
//            Console.WriteLine("Union: ");
//            foreach (int item in union)
//            {
//                Console.Write(item + " ");
//            }

//            // Intersection
//            HashSet<int> intersection = new HashSet<int>(set1);
//            intersection.IntersectWith(set2);
//            Console.WriteLine("\nIntersection: ");
//            foreach (int item in intersection)
//            {
//                Console.Write(item + " ");
//            }

//            //Difference(elements in set1 but not set2)
//            HashSet<int> difference = new HashSet<int>(set1);
//            difference.ExceptWith(set2);
//            Console.WriteLine("\nDifferece: ");
//            foreach(int item in difference)
//            {
//                Console.Write(item + " ");
//            }
//        }
//    }
//}
