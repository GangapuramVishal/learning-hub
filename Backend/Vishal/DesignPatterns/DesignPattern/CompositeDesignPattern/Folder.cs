namespace CompositeDesignPattern
{
    #region Composite - Folder
    /// <summary>
    /// The Folder class represents a composite in the hierarchy.
    /// It can contain both files and other folders.
    /// </summary>
    public class Folder : IFileSystemItem
    {
        private string _name;
        private List<IFileSystemItem> _items = new List<IFileSystemItem>();

        /// <summary>
        /// Initializes a new instance of the Folder class with the given name.
        /// </summary>
        /// <param name="name">The name of the folder.</param>
        public Folder(string name)
        {
            _name = name;
        }

        /// <summary>
        /// Adds a new item (file or folder) to the folder.
        /// </summary>
        public void Add(IFileSystemItem item)
        {
            _items.Add(item);
        }

        /// <summary>
        /// Removes an item from the folder.
        /// </summary>
        public void Remove(IFileSystemItem item)
        {
            _items.Remove(item);
        }

        /// <summary>
        /// Displays the folder and its contents recursively.
        /// </summary>
        public void Display(int depth)
        {
            Console.WriteLine(new string('-', depth) + _name);
            foreach (var item in _items)
            {
                item.Display(depth + 2);
            }
        }
    }
    #endregion
}
