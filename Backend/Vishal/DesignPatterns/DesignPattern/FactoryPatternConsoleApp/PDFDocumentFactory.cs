using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FactoryPatternConsoleApp
{
    public class PDFDocumentFactory : DocumentFactory
    {
        public override Document CreateDocument()
        {
            return new PDFDocument();
        }
    }
}
