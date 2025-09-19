namespace ProxyDesignPattern
{
    internal class Program
    {
        /// <summary>
        /// This is Client class that uses the Proxy to access the RealSubject.
        /// </summary>
        static void Main(string[] args)
        {
            Console.WriteLine("User with Admin role:");
            IDatabase adminProxy = new DatabaseProxy("Admin");
            adminProxy.FetchData();

            Console.WriteLine("\nUser with Guest role:");
            IDatabase guestProxy = new DatabaseProxy("Guest");
            guestProxy.FetchData();
        }
    }
}
