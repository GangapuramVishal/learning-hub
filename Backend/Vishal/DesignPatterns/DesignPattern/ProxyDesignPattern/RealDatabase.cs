namespace ProxyDesignPattern
{
    /// <summary>
    /// The RealSubject class that performs the actual operation.
    /// </summary>
    public class RealDatabase : IDatabase
    {
        public void FetchData()
        {
            Console.WriteLine("Fetching sensitive data from the database...");
        }
    }
}
