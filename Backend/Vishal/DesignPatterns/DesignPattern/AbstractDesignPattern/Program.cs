namespace AbstractFactory
{
    // 11. Demonstration of the Abstract Factory Pattern in action
    public class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Victorian Furniture:");
            IFurnitureFactory victorianFactory = new VictorianFactory();
            FurnitureStore victorianStore = new FurnitureStore(victorianFactory);
            victorianStore.ShowFurniture();

            Console.WriteLine("\nModern Furniture:");
            IFurnitureFactory modernFactory = new ModernFactory();
            FurnitureStore modernStore = new FurnitureStore(modernFactory);
            modernStore.ShowFurniture();
        }
    }
}
