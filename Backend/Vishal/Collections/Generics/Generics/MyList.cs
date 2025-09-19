//using System;
//using System.Collections.Generic;

//namespace Generics
//{
//    /*The Generic List<T> in C# is a Collection Class that belongs to System.Collections.Generic namespace. This Generic
//     *List<T> Collection Class represents a strongly typed list of objects which can be accessed by using the integer 
//     *index which is starting from 0. It also provides lots of methods that can be used for searching, sorting, and manipulating the list of items.
//     *
//     *Adding elements: You can add elements to the list using the Add() method.
//     *Removing elements: You can remove elements from the list using the Remove() method, which removes the first occurrence of a specific object, or the RemoveAt() method, which removes the element at a specified index.
//     *Accessing elements: You can access elements in the list using index notation, similar to arrays (e.g., list[index]).
//     *Iterating through elements: You can iterate through the elements in the list using a foreach loop or a for loop.
//     *Finding elements: You can find elements in the list using methods like Contains(), IndexOf(), or Find().
//     *Sorting: You can sort the elements in the list using methods like Sort().
//     */
//    internal class MyList
//    {
//        static void Main()
//        {
//            //Creating a Generic List of string type to store string elements
//            //List<string> books = new List<string>();

//            //adding elements 
//            //books.Add("Atomic Habit");
//            //books.Add("Happy Place");

//            //creating list using  collection initializer
//            List<string> books = new List<string>
//            {
//                "Happy Place",
//                "Absolution",
//                "Hang the Moon",
//                "When Crack Was King",
//                "As a Man Thinketh",
//                "The Country of the Blind"

//            };
//            DisplayList(books);

//            //removing an element
//            books.Remove("Absolution");
//            Console.WriteLine("\nBooks list after removing");
//            DisplayList(books);

//            //adding elements at specific index
//            books.Insert(3, "The Four Agreements");
//            Console.WriteLine("\nBooks list after inserting");
//            DisplayList(books);

//            //finding elements 
//            int index = books.IndexOf("The Country of the Blind");
//            if (index != -1)
//            {
//                Console.WriteLine($"\nFound at Index {index}");
//            }
//            else
//            {
//                Console.WriteLine("\nnot found in the list");
//            }

//            books.Sort();
//            Console.WriteLine("\nbooks list after sorting");
//            DisplayList(books);

//            static void DisplayList(List<string> books)
//            {
//                Console.WriteLine("\nBooks in the list");
//                foreach (string book in books)
//                {
//                    Console.WriteLine(book);
//                }
//            }
//        }
//    }
//}
