namespace CompositeDesignPattern
{
    #region Client
    /// <summary>
    /// The client code demonstrates the use of the Composite pattern
    /// to create and display a folder structure.
    /// </summary>
    class Program
    {
        static void Main(string[] args)
        {
            // Create individual files
            File file1 = new File("File1.txt");
            File file2 = new File("File2.txt");
            File file3 = new File("File3.doc");

            // Create folders and add files to them
            Folder folder1 = new Folder("Folder1");
            folder1.Add(file1);

            Folder folder2 = new Folder("Folder2");
            folder2.Add(file2);
            folder2.Add(file3);

            // Create the root folder and add other folders to it
            Folder root = new Folder("Root");
            root.Add(folder1);
            root.Add(folder2);

            // Display the entire folder structure
            root.Display(1);
        }
    }
    #endregion
}
