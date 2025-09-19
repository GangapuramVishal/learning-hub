//using System;
//using System.Collections;
//using System.Linq;
//using System.Text;
//using System.Threading.Tasks;

//namespace NonGenerics
//{
//    /*
//     * The Hashtable in C# is a Non-Generic Collection that stores the element in the form of “Key-Value Pairs”.
//     * The data in the Hashtable are organized based on the hash code of the key. The key in the HashTable is 
//     * defined by us and more importantly, that key can be of any data type. Once we created the Hashtable collection, 
//     * then we can access the elements by using the keys. The Hashtable class comes under the System.Collections namespace.
//     */


//    public class MyHashTable
//    {
//        public static void Main()
//        {
//            //Creating a Hashtable
//            Hashtable hashTable = new Hashtable();
//            hashTable.Add("Bobby", 1);
//            hashTable.Add("Vishal", 2);

//            //HashTable Elements
//            foreach (DictionaryEntry de in hashTable)
//            {
//                Console.WriteLine($" Key: {de.Key}, Value: {de.Value}");
//            }


//            //Creating a Hashtable using collection-initializer syntax
//            Hashtable citiesHashtable = new Hashtable()
//            {
//                {"UK", "London, Manchester, Birmingham"}, //Key:UK, Value:London, Manchester, Birmingham
//                {"USA", "Chicago, New York, Washington"}, //Key:USA, Value:Chicago, New York, Washington
//                {"India", "Mumbai, New Delhi, Pune"}      //Key:India, Value:Mumbai, New Delhi, Pune
//            };

//            citiesHashtable.Add("France", "Paris,Lyon, Marseille");

//            // Removing an element by key
//            citiesHashtable.Remove("USA");

//            Console.WriteLine("\n Is Country Exist in Keys : " + citiesHashtable.Contains("India"));

//            // Accessing an element by key
//            Console.WriteLine("\nAccessing the value for key 'India': " + citiesHashtable["India"]);

//            foreach (object obj in citiesHashtable.Keys)
//            {
//                Console.WriteLine(obj+ " : " + citiesHashtable[obj]);
//            }
//        }     
//    }
//}









////Note: 
////It implements the IDictionary interface.
////The Keys can be of any data type but they must be unique and not null.
////The Hashtable accepts both null and duplicate values.
////performance of the hashtable is less as compared to the ArrayList because of this key conversion (converting the key to an integer hashcode).
