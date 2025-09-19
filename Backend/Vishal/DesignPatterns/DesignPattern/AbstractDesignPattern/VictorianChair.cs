namespace AbstractFactory
{
    // 3. Concrete Product - Victorian Chair (Implements the abstract product interface.)
    /// <summary>
    /// Concrete implementation of the Chair for Victorian style.
    /// </summary>
    public class VictorianChair : IChair
    {
        public void SitOn()
        {
            Console.WriteLine("Sitting on a Victorian Chair.");
        }
    }
}
