namespace AbstractFactory
{
    // 7. Abstract Factory - FurnitureFactory (Declares methods for creating abstract products.)
    /// <summary>
    /// Declares the interface for creating abstract products.
    /// </summary>
    public interface IFurnitureFactory
    {
        IChair CreateChair();
        ITable CreateTable();
    }
}
