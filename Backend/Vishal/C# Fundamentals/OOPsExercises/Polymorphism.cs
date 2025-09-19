using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OOPsExercises
{
    internal class Polymorphism
    {
        /* Polymorphism is a Greek word that means multiple forms or shapes. You can use polymorphism if you want to
         * have multiple forms of one or more methods of a class with the same name.
           In C#, polymorphism can be achieved in two ways: 
           
           Compile-time Polymorphism (Method Overloading):
           This occurs when a class has multiple methods with the same name but different parameter types or numbers. 
           The compiler determines which method to call based on the arguments passed to it at compile time.

           Run-time Polymorphism (Method Overriding):
           This occurs when a subclass provides a specific implementation of a method that is already defined in its superclass.
           The method in the subclass overrides the method in the superclass. When you call the method on an instance of the superclass,
           the actual method executed depends on the type of the object at runtime.
         
         */
    }

    public class Calculator
    {
        public int Add(int number1, int number2)      //Method 1
        {
            return number1 + number2;
        }

        public double Add(double number1, double number2)    //Method 2
        {
            return number1 + number2;
        }
    }
    //Over ridding 
    class Vehicle
    {
        public virtual void StartEngine()
        {
            Console.WriteLine("vehicle engine started.");
        }
    }

    class Car : Vehicle
    {
        public override void StartEngine()
        {
            Console.WriteLine("Car engine started.");
        }
    }

    class AutoRickshaw : Vehicle
    {
        public override void StartEngine()
        {
            Console.WriteLine("AutoRickshaw engine started.");
        }
    }
}
