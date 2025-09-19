namespace AbstractFactory
{
    // 9. Concrete Factory - ModernFactory
    /// <summary>
    /// Concrete implementation of the FurnitureFactory for Modern style.
    /// </summary>
    public class ModernFactory : IFurnitureFactory
    {
        public IChair CreateChair()
        {
            return new ModernChair();
        }

        public ITable CreateTable()
        {
            return new ModernTable();
        }
    }
}
