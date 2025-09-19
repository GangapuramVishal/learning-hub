using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FactoryPatternConsoleApp
{
    public class WordDocumentFactory : DocumentFactory
    {
        public override Document CreateDocument()
        {
            return new WordDocument();
        }
    }
}
/*Concrete Factories
 * These concrete factories inherit from DocumentFactory and are responsible for 
 * creating specific document types.
 * 
 */