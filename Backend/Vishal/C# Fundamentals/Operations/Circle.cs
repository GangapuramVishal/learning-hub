using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Operations
{
    public class Circle
    {
        public void AreaPerimeter()
        {
            Console.WriteLine("enter the radius of circle: ");
            double radius = double.Parse(Console.ReadLine());
            var area = Math.PI * radius * radius;
            var perimeter = Math.PI * radius * 2;
            Console.WriteLine($"The area and perimeter of a circle are {area} and {perimeter}");
            Console.ReadLine();
            
            
        }
    }
}
