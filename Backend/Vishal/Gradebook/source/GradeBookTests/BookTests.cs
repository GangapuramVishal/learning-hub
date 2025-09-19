using GradeBook;

namespace GradeBookTests
{
    public class BookTests
    {
        [Fact]
        public void BookCalculatesAverageGrades()
        {
            //arrange 
            var book = new InMemoryBook("GradeBook");
            book.AddGrade(70.3);
            book.AddGrade(80.7);
            book.AddGrade(79.0);

            // act
            var result = book.GetStatistics();

            //assert
            Assert.Equal(76.7, result.Average,1); 
            Assert.Equal(80.7, result.High,1);
            Assert.Equal(70.3, result.Low,1);
            Assert.Equal('C', result.Letter);

        }
    }
}