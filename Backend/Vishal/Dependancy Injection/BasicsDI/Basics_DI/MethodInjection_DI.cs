using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Basics_DI
{
    internal class MethodInjection_DI
    {
        /* method injection is a form of dependency injection where dependencies are provided to a method rather than
         * to a class constructor. This allows for more fine-grained control over dependencies and can be particularly
         * useful in scenarios where a class has methods with different sets of dependencies.
         */
    }

    public interface IOperation
    {
        int Operate(int a, int b); //method
    }
    public class AdditionOperation : IOperation
    {
        public int Operate(int a, int b) 
        { 
            return a + b;
        
        }
    }
    public class SubstractionOperation : IOperation
    {
        public int Operate(int x, int y)
        {
            return x - y;
        }
    }
    public class MultiplicationOperation : IOperation
    {
        public int Operate(int x, int y)
        {
            return x * y;
        }
    }

    public class Calculator
    {
        public int Calculate(int a, int b, IOperation operation)
        {
            return operation.Operate(a, b);
        }
    }

}
