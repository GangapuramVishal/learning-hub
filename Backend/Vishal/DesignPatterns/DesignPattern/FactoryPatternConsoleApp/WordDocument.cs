using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FactoryPatternConsoleApp
{
    public class WordDocument : Document
    {
        public override void Open()
        {
            Console.WriteLine("Opening Word document...");
        }

        public override void Save()
        {
            Console.WriteLine("Saving Word document...");
        }
    }
}

/*Concrete Document Classes
 * Here, we create specific document types like Word and PDF. Each of these classes inherits from the 
 * Document class and provides its own implementation of the Open and Save methods
 */
