using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FactoryPatternConsoleApp
{
    public class PDFDocument : Document
    {
        public override void Open()
        {
            Console.WriteLine("Opening PDF document...");
        }

        public override void Save()
        {
            Console.WriteLine("Saving PDF document...");
        }
    }
}
