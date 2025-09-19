using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NonGenerics
{
    /*The Non-Generic SortedList Collection Class in C# represents a collection of key/value pairs that are sorted
     * by the keys and are accessible by key and by index. By default, it sorts the key/value pairs in ascending order. 
     * 
     * Add: Adds an element with a specified key and value to the sorted list.
     * Remove: Removes the element with the specified key from the sorted list.
     * Clear: Removes all elements from the sorted list.
     * ContainsKey: Determines whether the sorted list contains a specific key.
     * ContainsValue: Determines whether the sorted list contains a specific value.
     * IndexOfKey: Returns the index of the specified key in the sorted list.
     * IndexOfValue: Returns the index of the first occurrence of the specified value in the sorted list.
     * Keys: Gets a collection containing the keys in the sorted list.
     * Values: Gets a collection containing the values in the sorted list.
     */
    public class MySortedList
    {
        public static void Main()
        {
            //SortedList mySortedList = new SortedList();

            //Add(object key, object value):
            //mySortedList.Add(1, "One");
            //mySortedList.Add(2, "Two");

            //ading key/value pair in the SortedList using Collection Initializer
            Console.WriteLine("Adding elements to the sorted list:");
            SortedList mySortedList = new SortedList
            {
                {1, "One"},
                {2,"Two"},
                {3,"Three"},
                {4,"Four"},
                {5,"Five"}
            };
            DisplaySortedList(mySortedList);

            // Check if the sorted list contains a specific key
            Console.WriteLine("\nChecking if the sorted list contains a specific key:");
            int keyToCheck = 2;
            Console.WriteLine($"Does the sorted list contain the key '{keyToCheck}'? {mySortedList.ContainsKey(keyToCheck)}");

            // Check if the sorted list contains a specific value
            Console.WriteLine("\nChecking if the sorted list contains a specific value:");
            var valueToCheck = "Three";
            Console.WriteLine($"Does the sorted list contain the value '{valueToCheck}'? {mySortedList.ContainsValue(valueToCheck)}");

            // Get the index of a specific key
            Console.WriteLine("\nGetting the index of a specific key:");
            Console.WriteLine($"Index of key '4': {mySortedList.IndexOfKey(4)}");

            // Get the index of a specific value
            Console.WriteLine("\nGetting the index of a specific value:");
            Console.WriteLine($"Index of value 'Five': {mySortedList.IndexOfValue("Five")}");

            // Get a collection of keys
            Console.WriteLine("\nGetting a collection of keys:");
            ICollection keys = mySortedList.Keys;
            foreach (var key in keys)
            {
                Console.WriteLine("Key: " + key);
            }

            // Get a collection of values
            Console.WriteLine("\nGetting a collection of values:");
            ICollection values = mySortedList.Values;
            foreach (var value in values)
            {
                Console.WriteLine("Value: " + value);
            }

            // Remove an element from the sorted list
            Console.WriteLine("\nRemoving an element from the sorted list:");
            mySortedList.Remove(3);
            DisplaySortedList(mySortedList);

            // Clear the sorted list
            Console.WriteLine("\nClearing the sorted list");
            mySortedList.Clear();
            Console.WriteLine("\nIs sortedList empty? " + (mySortedList.Count == 0));

            // Method to display the contents of the sorted list
            static void DisplaySortedList(SortedList mySortedList)
            {
                Console.WriteLine("Sorted list contents:");
                foreach (DictionaryEntry entry in mySortedList)
                {
                    Console.WriteLine(entry.Key + ": " + entry.Value);
                }
            }

        }
    }
}






















/*
 * Notes: 
 * The Non-Generic SortedList object internally maintains two arrays to store the elements of the list,
 * i.e, one array for the keys and another array for the associated values. Here, the key cannot be null,
 * but the value can be null. And one more, it does not allow duplicate keys.
 * 
 * In the same SortedList, it is not possible to store keys of different data types. If you try then the compiler will throw an exception.
 */