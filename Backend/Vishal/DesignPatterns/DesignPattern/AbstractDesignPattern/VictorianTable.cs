namespace AbstractFactory
{
    // 4. Concrete Product - Victorian Table (Implements the abstract product interface.)
    /// <summary>
    /// Concrete implementation of the Table for Victorian style.
    /// </summary>
    public class VictorianTable : ITable
    {
        public void Use()
        {
            Console.WriteLine("Using a Victorian Table.");
        }
    }
}
