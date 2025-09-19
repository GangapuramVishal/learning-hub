using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GradeBook
{
    public class Program
    {
        static void Main(string[] args)        // main method
        {
            IBook book = new DiskBook("GradeBook");     //Creating an instance
            book.GradeAdded += OnGradeAdded;
            EnterGrades(book);
            {
                var stats = book.GetStatistics();

                Console.WriteLine($"For the book named {book.Name}");
                Console.WriteLine($"The Lowest Grades are {stats.Low}");
                Console.WriteLine($"The Highest Grades are {stats.High}");
                Console.WriteLine($"The averag grade is {stats.Average:N1}");
                Console.WriteLine($"The letter grade is {stats.Letter}");

            }
            static void OnGradeAdded(object sender, EventArgs e)
            {
                Console.WriteLine("A Grade was added");
            }
        }

    private static void EnterGrades(IBook book)
        {
            while (true)
            {
                Console.WriteLine("Enter a grade or enter 'q' to quit.");
                var input = Console.ReadLine();

                if (input == "q")
                {
                    break;
                }

                try
                {
                    var grade = double.Parse(input);
                    book.AddGrade(grade);
                }
                catch (ArgumentException ex)
                {
                    Console.WriteLine(ex.Message);
                }
                catch (FormatException ex)
                {
                    Console.WriteLine(ex.Message);

                }
                finally
                {
                    System.Console.WriteLine("**");
                }

            }
        }
    }
}
