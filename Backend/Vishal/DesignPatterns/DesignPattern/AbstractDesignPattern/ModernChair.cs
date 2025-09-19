namespace AbstractFactory
{
    // 5. Concrete Product - Modern Chair(Implements the abstract product interface.)
    /// <summary>
    /// Concrete implementation of the Chair for Modern style.
    /// </summary>
    public class ModernChair : IChair
    {
        public void SitOn()
        {
            Console.WriteLine("Sitting on a Modern Chair.");
        }
    }
}
