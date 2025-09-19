//using AutoFixture;
//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Text;
//using System.Threading.Tasks;

//namespace TestAutomation
//{
//    /*AutoFixture is an open source library for .NET designed to minimize the 'Arrange' phase of your unit tests
//     * in order to maximize maintainability. Its primary goal is to allow developers to focus on what is being
//     * tested rather than how to setup the test scenario, by making it easier to create object graphs containing test data.
//     */

//    class Employee
//    {
//        public int salary { get; set; }
//        public string designation { get; set; }
//        public List<string> position { get; set; }

//    }
//    public class AutoFixtureTest
//    {
//        static void Main(string[] args)
//        {
//            var fixture = new Fixture();
//            string name = fixture.Create<string>();
//            Console.WriteLine(name);

//            var emp = fixture.Create<Employee>();
//            Console.WriteLine("salary is " + emp.salary + " and designation is " + emp.designation);

//            foreach(var item in emp.position)
//            {
//                Console.WriteLine(item);
//            }
//        }
//    }
//}
