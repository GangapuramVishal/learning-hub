//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Text;
//using System.Threading.Tasks;

//namespace DelegatesPOC2
//{
//    public delegate void PrintDel(int y);
//    public delegate void ToReturnMethod(string message);
//    public class CallBackExample
//    {
//        public static void Main(string[] args)
//        {
//            CallBackExample example = new CallBackExample();
//            example.PrintHelper(100, example.PrintMoney);  //giving method as a parameter inside other method.
//                                                           //PrintMoney methos is callback function

//            example.PrintHelper(50, example.PrintNumber);

//            ToReturnMethod obj = example.TypeOfDelegate();   //obj contains the defination of Method
//            obj("Method is called by using a instance of type ToReturnMethod");
//        }


//        public void PrintMoney(int price)
//        {
//            Console.WriteLine("Price is ${0}", price);
//        }
//        public void PrintNumber(int number)
//        {
//            Console.WriteLine("Number is ${0}", number);
//        }
//        public void PrintHelper(int x, PrintDel Delparameter) //parameter of type PrintDel allow us to pass a method of type PrintDel
//        {
//            Delparameter(x);
//        }



//        //to return a method from inside another method

//        public static void Method(string msg)
//        {
//            Console.WriteLine("I have returned from a Method");
//        }

//        public ToReturnMethod TypeOfDelegate()
//        {
//            return Method;
//        }
//    }
//}
