namespace AbstractFactory
{
    // 10. Client
    /// <summary>
    /// The client class that interacts with abstract factories to produce furniture.
    /// </summary>
    public class FurnitureStore
    {
        private readonly IChair _chair;
        private readonly ITable _table;

        /// <summary>
        /// Constructor that takes an abstract factory and creates abstract products.
        /// </summary>
        public FurnitureStore(IFurnitureFactory factory)
        {
            _chair = factory.CreateChair();
            _table = factory.CreateTable();
        }

        /// <summary>
        /// Displays the furniture available in the store.
        /// </summary>
        public void ShowFurniture()
        {
            _chair.SitOn();
            _table.Use();
        }
    }
}
