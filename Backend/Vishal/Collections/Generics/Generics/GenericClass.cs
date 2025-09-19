namespace Generics
{
    public class GenericClass<T>  //type parameter
    {
        //field value of type T
        private T value;
        public GenericClass(T val)
        {
            this.value = val;
        }

        public T GetValue()
        {
            return value;
        }
    }

    public class Program
    {
        static void Main(string[] args)
        {
            //instance of generic class 
            GenericClass<int> num = new GenericClass<int>(1);
            Console.WriteLine("value stored in num: " + num.GetValue());

            GenericClass<string> Name = new GenericClass<string>("Bobby");
            Console.WriteLine("Value stored in Name: " + Name.GetValue());
        }
    }
}
