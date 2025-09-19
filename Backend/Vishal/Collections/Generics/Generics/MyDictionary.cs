//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Text;
//using System.Threading.Channels;
//using System.Threading.Tasks;

//namespace Generics
//{
//    /*In C#, a generic Dictionary<TKey, TValue> is a collection that stores key-value pairs, where each key must be unique
//     *within the dictionary. It provides fast lookup of values based on their associated keys. The keys are used to access 
//     *corresponding values efficiently, making it suitable for scenarios where you need to associate data with unique identifiers.
//     * Dictionary<TKey, TValue>
//     * 
//     * 
//     */
//    public class MyDictionary
//    {
//        public static void Main()
//        {
//            Dictionary<int, string> studentDict = new Dictionary<int, string>();

//            //Adding key-value pair's
//            studentDict[1] = "Bob";
//            studentDict[3] = "Alice";
//            studentDict[2] = "John";
//            studentDict[5] = "Arun";
//            studentDict[6] = "Jenny";
//            DisplayDict(studentDict);

//            //accessing value by key
//            Console.WriteLine("\nEnter the Id to find name");
//            int id = int.Parse(Console.ReadLine());
//            if (studentDict.ContainsKey(id))
//            {
//                var name = studentDict[id];
//                Console.WriteLine($"\n{id} is the Id of {name}");
//            }
//            else
//            {
//                Console.WriteLine($"\n{id} not found in the student dictionary");
//            }

//            //updating value
//            Console.WriteLine("\nEnter the Id to update name");
//            int stdid = int.Parse(Console.ReadLine());
//            if (studentDict.ContainsKey(stdid))
//            {
//                Console.WriteLine("\nEnter the name to update");
//                var name = Console.ReadLine();
//                studentDict[stdid] = name;
//                Console.WriteLine($"\nId num {stdid} is updated to {name}");
//            }
//            else
//            {
//                Console.WriteLine($"\n{stdid} not found, can't update");
//            }
//            DisplayDict(studentDict);

//            //removing key-value
//            Console.WriteLine("\nEnter the Id to detele name");
//            int removeid = int.Parse(Console.ReadLine());
//            if (studentDict.ContainsKey(removeid))
//            {
//                studentDict.Remove(removeid);
//            }
//            else
//            {
//                Console.WriteLine($"\n{removeid} not found, can't delete");
//            }
//            DisplayDict(studentDict);


//            static void DisplayDict(Dictionary<int, string> studentDict)
//            {
//                Console.WriteLine("\nstudents in the dictionary");
//                foreach (KeyValuePair<int, string> kvp in studentDict)
//                {
//                    Console.WriteLine($"Key: {kvp.Key}, Value: {kvp.Value}");
//                }
//            }
//        }
//    }
//}


















///*Keys: Keys must be unique within the dictionary. They are used to retrieve corresponding values.
// * Values: Values can be of any type and can be accessed using their associated keys.
// * Performance: Dictionary<TKey, TValue> provides fast retrieval of values based on keys, typically with O(1) complexity for lookup operations.
// * Mutability: You can add, remove, and update key-value pairs in the dictionary.
// */