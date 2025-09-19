namespace DelegatesPOC
{
    public class Program
    {
        static void Main(string[] args)
        {
            //WithoutDelegates

            //WithoutDelegates obj = new WithoutDelegates();
            //obj.AddNumbers(20, 300);

            //var str = WithoutDelegates.SayHello("Bobby");
            //Console.WriteLine(str);

            //WithDelegate's

            WithDelegates obj = new WithDelegates();

            //address present in the AddNumbers is given to ad
            AddDelegate ad = new AddDelegate(obj.AddNumbers);
            ad(70, 30);

            SayDelegate sd = new SayDelegate(WithDelegates.SayHello);
            string str = sd("vishal");
            Console.WriteLine(str);
        }
    }
}
