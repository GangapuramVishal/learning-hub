using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DI_Using_Containers
{
    public class Rectangle
    {
        private readonly IAreaCalculator _areaCalculator; //private field
        private readonly IPerimeterCalculator _perimeterCalculator; //private field
        

        public Rectangle(IAreaCalculator areaCalculator, IPerimeterCalculator perimeterCalculator)  //dependencies
        {
            _areaCalculator = areaCalculator;
            _perimeterCalculator = perimeterCalculator;   //assign the values of the constructor parameters to private fields of the Rectangle class
        }
        public double GetArea(double length, double width)
        {
            return _areaCalculator.CalculateArea(length, width);
        }
        public double GetPerimeter(double length, double width)
        {
            return _perimeterCalculator.CalculatePerimeter(length, width);
        }
    }
}


/* NOTE: The Rectangle class depends on IAreaCalculator and IPerimeterCalculator interfaces to calculate the
 * area and perimeter of a rectangle. Instead of creating instances of concrete classes directly within the 
 * Rectangle class, the dependencies are injected into the class through its constructor.
 */