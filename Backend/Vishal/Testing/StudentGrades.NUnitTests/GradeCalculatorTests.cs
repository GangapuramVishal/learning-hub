using NUnit.Framework;
using StudentGrades_ConsoleApp;

namespace StudentGrades.NUnitTests
{
    public class GradeCalculatorTests
    {
        private GradeCalculator _gradeCalculator { get; set; } = null;

        [SetUp]
        public void Setup()
        {
            _gradeCalculator = new GradeCalculator();
        }

        [TestCase(95)]
        [TestCase(90)]
        [TestCase(100)]
        public void GetGradeByPercentage_EqualTest(int percentage)
        {
            //Assign
            //var percentage = 87;

            //Act
            var grade = _gradeCalculator.GetGradeByPercentage(percentage);

            //Assert
            Assert.AreEqual("A", grade);
            //Assert.Pass();
        }
        //Not equal test
        [TestCase(-89)]
        [TestCase(25)]
        [TestCase(78)]
        public void GetGradeByPercentage_NotEqualTest(int percentage)
        {
            //Assign
            //var percentage = 87;

            //Act
            var grade = _gradeCalculator.GetGradeByPercentage(percentage);

            //Assert
            Assert.AreNotEqual("A", grade);
            //Assert.Pass();
        }
    }
}