namespace DelegatesPOC2
{
    public class Program
    {
        //creating a delegate
        public delegate int Sum(int x, int y);
        public delegate void DisplayDel(string message);
        static void Main(string[] args)
        {

            Program p = new Program();

            //instanciating a delegate 
            Sum sum = new Sum(p.Addition);     //sum stores the address present in Addition,
            int result = sum(10, 20);
            Console.WriteLine("Sum is " + result);


            DisplayDel display = new DisplayDel(p.Display);
            display("This is a delegate");      //we have to invoke a delegate
            display.Invoke("This a other way to invoke");
        }
        public int Addition(int a, int b)     //Addition stores the address(present in heap) of method in stack
        {
            return a + b;
        }

        public void Display(string msg)
        {
            Console.WriteLine(msg);
        }
    }
}
