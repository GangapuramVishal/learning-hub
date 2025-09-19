using System;

namespace Records_Classes
{
    internal class Program
    {
        static void Main(string[] args)
        {
            //--------------------PersoAsClass------------------------------------------------

            var personC1 = new PersonAsClass("John", "Deo");
            var personC2 = new PersonAsClass("John", "Deo");
            Console.WriteLine(personC1 == personC2); // False, because classes use reference equality

            //mutability
            var person = new PersonAsClass("John", "Doe");
            person.FirstName = "Jane"; //Alloed

            //--------------------PersonAsRecord-----------------------------------------------

            var personR1 = new PersonAsRecord("Bob", "Josh");
            var personR2 = new PersonAsRecord("Bob", "Josh");
            Console.WriteLine(personR1 == personR2); // True, because records use value equality

            //Immutability
            var personI = new PersonAsRecord("John", "Doe");
            // personI.FirstName = "Jane";          // Not allowed, properties are read-only by default
        }
    }
}
