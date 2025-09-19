namespace PrototypeDesignPattern
{
    /// <summary>
    /// The abstract Prototype class that defines the Clone method.
    /// All concrete prototypes will implement this interface.
    /// </summary>
    public abstract class CharacterPrototype
    {
        /// <summary>
        /// Clone method to create a copy of the current instance.
        /// </summary>
        /// <returns>A new instance of CharacterPrototype</returns>
        public abstract CharacterPrototype Clone();
    }
}
