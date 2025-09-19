namespace ProxyDesignPattern
{
    /// <summary>
    /// The common interface shared by the RealSubject and Proxy.
    /// Defines the method that clients will call.
    /// </summary>
    public interface IDatabase
    {
        void FetchData();
    }
}
