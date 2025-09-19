//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Security.Cryptography.X509Certificates;
//using System.Text;
//using System.Threading.Tasks;
///*
// *A MultiCast Delegate is a delegate that has a reference to multiple functions at the same time. When we invoke a multiCast delegate,
// *All the functions the delegate is pointing to are invoked.
// */
//namespace DelegatesPOC2
//{
//    public delegate void Del();
//    public class MultiCastDelegate
//    {

//        public static void Main(string[] args)
//        {
//            MultiCastDelegate p = new MultiCastDelegate();

//            //1st way for multicasting using multiple instance of Del
//            //Del del1, del2, del3, del4;   //reference variables of type Del

//            //del1 = new Del(p.MethodOne);
//            //del2 = new Del(p.MethodTwo);
//            //del3 = new Del(p.MethodThree);

//            //del4 = del1 + del2 + del3;     //del4 is pointing to all 3 methods & del4 it is multicast delegate

//            //del4();

//            //2nd way of multicasting using single instance of Del
//            Del del = new Del(p.MethodOne);
//            del += p.MethodTwo;
//            del += p.MethodThree;

//            del();
//        }
//        public void MethodOne()
//        {
//            Console.WriteLine("methodOne Called");
//        }
//        public void MethodTwo()
//        {
//            Console.WriteLine("methodTwo Called");
//        }
//        public void MethodThree()
//        {
//            Console.WriteLine("methodThree Called");
//        }
//    }
//}
