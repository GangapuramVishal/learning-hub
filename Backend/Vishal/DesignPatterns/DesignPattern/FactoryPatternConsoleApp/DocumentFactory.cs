using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FactoryPatternConsoleApp
{
    public abstract class DocumentFactory
    {
        public abstract Document CreateDocument();
    }
}
/*Document Factory
 * This is an abstract class that defines a method CreateDocument which will be
 * responsible for creating instances of documents. The actual creation logic will be defined in the subclasses.
 * 
 */