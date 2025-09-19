namespace AbstractFactory
{
    // 6. Concrete Product - Modern Table (Implements the abstract product interface.)
    /// <summary>
    /// Concrete implementation of the Table for Modern style.
    /// </summary>
    public class ModernTable : ITable
    {
        public void Use()
        {
            Console.WriteLine("Using a Modern Table.");
        }
    }
}
