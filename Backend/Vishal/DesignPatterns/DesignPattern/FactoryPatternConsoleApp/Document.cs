using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FactoryPatternConsoleApp
{
    //This abstract class is like a blueprint for all types of documents.
    //It defines common methods that all documents should have, such as Open and Save.
    public abstract class Document
    {
        public abstract void Open();
        public abstract void Save();
    }
}
