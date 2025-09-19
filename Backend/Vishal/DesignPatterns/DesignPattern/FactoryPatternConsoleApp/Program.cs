namespace FactoryPatternConsoleApp
{
    public class Program
    {
        static void Main(string[] args)
        {
            DocumentFactory factory = null;
            string fileType = "PDF";

            if (fileType == "Word")
            {
                factory = new WordDocumentFactory();
            }
            else if (fileType == "PDF")
            {
                factory = new PDFDocumentFactory();
            }
            // Using the factory to create a document
            Document document = factory.CreateDocument();
            document.Open();
            document.Save();
        }
    }
}
