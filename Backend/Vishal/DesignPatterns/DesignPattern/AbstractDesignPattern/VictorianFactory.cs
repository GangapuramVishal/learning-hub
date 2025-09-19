namespace AbstractFactory
{
    // 8. Concrete Factory - VictorianFactory
    /// <summary>
    /// Concrete implementation of the FurnitureFactory for Victorian style.
    /// </summary>
    public class VictorianFactory : IFurnitureFactory
    {
        public IChair CreateChair()
        {
            return new VictorianChair();
        }

        public ITable CreateTable()
        {
            return new VictorianTable();
        }
    }
}
