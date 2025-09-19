using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace OOPsExercises
{
    internal class Inheritance
    {
       /* Inheritance is a fundamental concept in object-oriented programming(OOP) that allows a new
        * class (derived class or subclass) to inherit properties and behaviors from an existing 
        * class (base class or superclass). This facilitates code reuse
        * 
        * There are several types of inheritance:
        
        * Single Inheritance: A derived class inherits from only one base class.
        * Multilevel Inheritance: A derived class inherits from a base class, and another class inherits from this derived class, forming a chain of inheritance.
        * Hierarchical Inheritance: Multiple classes inherit from a single base class.
        * Hybride Inheritance: It's a combination of multiple and multi-level inheritance
        */
    }
    //Base Class
    public class Animal
    {
        public void Eat()
        {
            Console.WriteLine("Animal is eating");
        }
        public void Run()
        {
            Console.WriteLine("Animal is running");
        }

    }

    //Derived class with single-inheritance
    public class Dog : Animal
    {
        public void Bark()
        {
            Console.WriteLine("Bow.. Bowww.....");
        }
    }

    //Derived class with multi-level inheritance
    public class GoldenRetriver : Dog 
    {
        public void Plays()
        {
            Console.WriteLine("Golden Retriver is a friendly dog");
        }
    }

    //Derived class with  hierarchical inheritance one base, many derived
    public class Cat : Animal
    {
        public void sound()
        {
            Console.WriteLine("meow... meoww..");
        }
    }





}
