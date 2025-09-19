namespace CompositeDesignPattern
{
    #region Leaf - File
    /// <summary>
    /// The File class represents a leaf node in the composite structure.
    /// It cannot contain other items.
    /// </summary>
    public class File : IFileSystemItem
    {
        private string _name;

        /// <summary>
        /// Initializes a new instance of the File class with the given name.
        /// </summary>
        /// <param name="name">The name of the file.</param>
        public File(string name)
        {
            _name = name;
        }

        /// <summary>
        /// Displays the name of the file with indentation based on depth.
        /// </summary>
        public void Display(int depth)
        {
            Console.WriteLine(new string('-', depth) + _name);
        }
    }
    #endregion
}
