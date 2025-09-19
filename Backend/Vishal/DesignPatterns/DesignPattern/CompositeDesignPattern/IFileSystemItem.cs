using System;
using System.Collections.Generic;

namespace CompositeDesignPattern
{
    #region Component
    /// <summary>
    /// The Component interface defines operations common to both
    /// simple and composite objects in the hierarchy.
    /// </summary>
    public interface IFileSystemItem
    {
        /// <summary>
        /// Displays the name of the item and its children (if any).
        /// </summary>
        void Display(int depth);
    }
    #endregion
}
