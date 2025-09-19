namespace ProxyDesignPattern
{
    /// <summary>
    /// The Proxy class that controls access to the RealSubject.
    /// Implements the same interface as the RealSubject.
    /// </summary>
    public class DatabaseProxy : IDatabase
    {
        private RealDatabase _realDatabase;
        private readonly string _userRole;

        /// <summary>
        /// Initializes a new instance of the <see cref="DatabaseProxy"/> class.
        /// </summary>
        /// <param name="userRole">The role of the user accessing the database.</param>
        public DatabaseProxy(string userRole)
        {
            _userRole = userRole;
        }

        public void FetchData()
        {
            if (_userRole == "Admin")
            {
                Console.WriteLine("Access granted. Initializing real database...");
                _realDatabase ??= new RealDatabase();
                _realDatabase.FetchData();
            }
            else
            {
                Console.WriteLine("Access denied. You do not have sufficient permissions.");
            }
        }
    }
}
