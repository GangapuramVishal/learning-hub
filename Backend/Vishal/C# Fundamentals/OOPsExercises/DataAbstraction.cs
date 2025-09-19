using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OOPsExercises
{
    internal class DataAbstraction
    {

        /* Data abstraction in C# refers to the concept of hiding the complex implementation details
         * and showing only the essential features of an object to the outside world. It allows us to 
         * focus on what an object does rather than how it does it. */
    }

    // Abstract class defining a shape
    public abstract class Shape
    {
        // Abstract method to calculate area (no implementation)
        public abstract double CalculateArea();

    }
    public class Rectangle: Shape
    {
        // Properties representing dimensions of the rectangle

        public double Length { get; set; }
        public double Width { get; set; }

        // Implementation of CalculateArea method for rectangle
        public override double CalculateArea()
        {
            return Length * Width;
        }
    }
    // Concrete class representing a circle
    public class Circle : Shape
    {
        // Property representing the radius of the circle
        public double Radius { get; set; }

        // Implementation of CalculateArea method for circle
        public override double CalculateArea()
        {
            return Math.PI * Radius * Radius;
        }
    }
}
