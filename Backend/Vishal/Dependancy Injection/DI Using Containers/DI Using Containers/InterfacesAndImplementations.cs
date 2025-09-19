using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DI_Using_Containers
{
    internal class InterfacesAndImplementations
    {
        /* The IoC container that is also known as a DI Container is a framework for implementing 
         * automatic dependency injection very effectively. It manages the complete object creation
         * and its lifetime, as well as it also injects the dependencies into the classes. 
         * We can also manage application dependencies without a DI Container, but it will
         * be as "POOR MAN’S DI" and we have to do more work, to make it configured and manageable
         */
    }

    //This class contains interfaces and it's implementation's
    public interface IAreaCalculator                              //interface for calculating area
    {
        double CalculateArea(double length, double width);
    }
    public interface IPerimeterCalculator                        //interface for calculating perimeter
    {
        double CalculatePerimeter(double length, double width);
    }

    // Implementation for calculating area
    public class AreaCalculator : IAreaCalculator
    {
        public double CalculateArea(double length, double width)
        {
            return length * width;
        }
    }
    // Implementation for calculating perimeter
    public class PerimeterCalculator : IPerimeterCalculator
    {
        public double CalculatePerimeter(double length, double width)
        {
            return 2*(length + width);
        }
    }

}
