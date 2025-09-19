using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Intrinsics.X86;
using System.Text;
using System.Threading.Tasks;

namespace GradeBook
{
    public delegate void GradeAddedDelegate(object sender, EventArgs args);

    public class NamedObject
    {
        public NamedObject(string name)   //constructor
        {
            Name = name;
        }

        public string Name
        {
            get;
            set;
        }
    }

    public interface IBook
    {
        void AddGrade(double grade);
        Statistics GetStatistics();
        string Name { get; }
        event GradeAddedDelegate GradeAdded;
    }
    public abstract class Book : NamedObject, IBook  // adding interface IBook
    {
         public Book(string name) : base(name)
        {
        }

        public abstract event GradeAddedDelegate GradeAdded;

        public abstract void AddGrade(double grade);

        public abstract Statistics GetStatistics();
        
    }
    public class DiskBook : Book
    {
        public DiskBook(string name) : base(name) 
        {
        }

        public override event GradeAddedDelegate GradeAdded;

        public override void AddGrade(double grade)
        {
            using (var writer = File.AppendText($"{Name}.txt"))   // appends the provided txt at the end of the file 
            {
                writer.WriteLine(grade);            // when we invoke addgrade method our code will open a file for writing
                if(GradeAdded != null)              //writes a line into the file this will contain a grade value
                {
                    GradeAdded(this, new EventArgs());
                }                                    
            }

        }

         public override Statistics GetStatistics()
        {
            var result = new Statistics();

            using(var reader = File.OpenText($"{Name}.txt"))
            {
                var line = reader.ReadLine();
                while (line != null) 
                {
                    var number = double.Parse(line);
                    result.Add(number);
                    line = reader.ReadLine();
                }
            }

            return result;
        }
    }
    public class InMemoryBook : Book
    {      
        public InMemoryBook(string name) : base(name)  //Constructor
        {
            grades = new List<double>();
            Name = name;
        }
        public void AddGrade(char letter)
        {
            switch (letter)
            {
                case 'A':
                    AddGrade(90);
                    break;

                case 'B':
                    AddGrade(80);
                    break;

                case 'C':
                    AddGrade(70);
                    break;

                default:
                    AddGrade(0);
                    break;
            }
        }
        public override void AddGrade(double grade)
        {
            if (grade <= 100 && grade >= 0)
            {
                grades.Add(grade);
                if(GradeAdded != null)
                {
                    GradeAdded(this, new EventArgs());
                }
            }
            else
            {
                throw new ArgumentException($"Invalid {nameof(grade)}");
            }
            
        }
        public override event GradeAddedDelegate GradeAdded; //we are overriding the implementation of base class
        public override Statistics GetStatistics() // we are overriding what's available in our base cass
        {
            var result = new Statistics();
            

            for (var index = 0; index < grades.Count; index++)
            {
                result.Add(grades[index]);
            }

             return result;
        }
        private List<double> grades;

        public const string CATEGORY = "Science";

    }
}