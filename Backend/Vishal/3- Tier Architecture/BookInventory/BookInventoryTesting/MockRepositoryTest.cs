using BookInventorty.DAL.Data;
using BookInventorty.DAL.Models;
using BookInventorty.DAL.Repositorys;
using Microsoft.EntityFrameworkCore;
using MockQueryable.Moq;
using Moq;

namespace BookInventoryTesting
{
    [TestFixture]
    public class MockRepositoryTest
    {
        private Mock<BookDbContext> _mockDbContext;
        private BookRepository _bookRepository;
        public static List<Book> testBookData = new List<Book>
            {
                new Book { Title = "The Great Gatsby", Author = "F. Scott Fitzgerald", Genre = "Fiction", Quantity = 10 },
                new Book { Title = "To Kill a Mockingbird", Author = "Harper Lee", Genre = "Fiction", Quantity = 8 },
                new Book { Title = "1984", Author = "George Orwell", Genre = "Science Fiction", Quantity = 12 },
                new Book { Title = "Pride and Prejudice", Author = "Jane Austen", Genre = "Romance", Quantity = 15 }
            };
        public IQueryable<Book> _bookquery = testBookData.AsQueryable();

        [SetUp]
        public void SetUp()
        {
             

            var options = new DbContextOptions<BookDbContext>();
            _mockDbContext = new Mock<BookDbContext>(options);

            var mockBooksDbSet = _bookquery.AsQueryable().BuildMockDbSet();
            _mockDbContext.Setup(b => b.Set<Book>()).Returns(mockBooksDbSet.Object);
            _bookRepository = new BookRepository(_mockDbContext.Object);

        }

        [Test]
        public void GetAll_ReturnAllBooks()
        {

            //Arrage
            

            //Act
            var result = _bookRepository.GetAllDetails();

            //Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(testBookData, result);

        }
    }
}


//        [SetUp]
//        public void Setup()
//        {

//            var options = new DbContextOptions<BookDbContext>();
//            _mockDbContext = new Mock<BookDbContext>(options);
            

//            // Creating an instance of BookRepository with mocked DbContext
//            _bookRepository = new BookRepository(_mockDbContext.Object);
//        }

//        [Test]
//        public void GetBookById_ExistingId_ReturnsBook()
//        {
//            // Arrange
//            int bookId = 1;
//            var book = new Book { Id = bookId, Title = "The Great Gatsby", Author = "F. Scott Fitzgerald", Genre = "Fiction", Quantity = 10 };
//            var mockDbSet = book.BuildMockDbSet();
//                //mockDbSet.Setup(x => x.Find(bookId)).Returns(book.Id);
//            _mockDbContext.Setup(x => x.Set<Book>()).Returns(mockDbSet.Object);

//            // Act
//            var result = _bookRepository.GetByIdDetails(bookId);

//            // Assert
//            Assert.IsNotNull(result);
//            Assert.AreEqual(bookId, result.Id);
//        }
//    }
//}

//[Test]
//public void GetAllDetails_ReturnsAllBooks()
//{
//    // Arrange
//    var books = new List<Book>
//        {
//            new Book { Id = 1, Title = "The Great Gatsby", Author = "F. Scott Fitzgerald", Genre = "Fiction", Quantity = 10 },
//            new Book { Id = 2, Title = "To Kill a Mockingbird", Author = "Harper Lee", Genre = "Fiction", Quantity = 8 },
//            new Book { Id = 3, Title = "1984", Author = "George Orwell", Genre = "Science Fiction", Quantity = 12 },
//            new Book { Id = 4, Title = "Pride and Prejudice", Author = "Jane Austen", Genre = "Romance", Quantity = 15 }
//        };
//    var mockDbSet = books.AsQueryable().BuildMockDbSet();
//    _mockDbContext.Setup(x => x.Set<Book>()).Returns(mockDbSet.Object);

//    // Act
//    var result = _bookRepository.GetAllDetails();

//    // Assert
//    Assert.IsNotNull(result);
//    Assert.AreEqual(4, result.Count);
//}
//        }
//}





















//public BookDbContext _bookDbContext;

//public BookRepository _bookRepository;

//public Mock<BookDbContext> _mockDbContext;

//public static List<Book> _booksList = new List<Book>()
//{
//    new Book() { Id = 1, Title = "Book 1", Author = "Author 1", Genre = "Fiction", PublicationDate = new DateTime(2020, 1, 1), Quantity = 5 },
//    new Book() { Id = 2, Title = "Book 2", Author = "Author 2", Genre = "Non-Fiction", PublicationDate = new DateTime(2021, 3, 15), Quantity = 3 }
//};

//public IQueryable<Book> _bookQueryable = _booksList.AsQueryable();

//[SetUp]
//public void setUpMockData()
//{
//    var options = new DbContextOptions<BookDbContext>();

//    _mockDbContext = new Mock<BookDbContext>(options);

//    var mockBookDbSet = _bookQueryable.AsQueryable().BuildMockDbSet();

//    _mockDbContext.Setup(x => x.Set<Book>()).Returns(mockBookDbSet.Object);

//    _bookRepository = new BookRepository(_mockDbContext.Object);
//}


////[Test]
////public void GetAllDetails_ReturnsListOfBooks_WhenBooksExist()
////{
////    // Arrange
////    var expectedResult = _booksList; // Expected list of books

////    // Act 
////    var result = _bookRepository.GetAllDetails();

////    // Assert
////    Assert.IsNotNull(result);
////    Assert.AreEqual(expectedResult.Count, result.Count);
////    for (int i = 0; i < expectedResult.Count; i++)
////    {
////        Assert.AreEqual(expectedResult[i].Id, result[i].Id);
////        Assert.AreEqual(expectedResult[i].Title, result[i].Title);
////        Assert.AreEqual(expectedResult[i].Author, result[i].Author);

////    }
////}

//[Test]
//public void GetEmployeeByIdAsync_ExistingId_ReturnsEmployee()
//{

//    // Act
//    var employeeId = 104;
//    var result =  _bookRepository.GetByIdDetails(employeeId);

//    // Assert
//    Assert.IsNotNull(result);
//    Assert.AreEqual(employeeId, result.Id);

//}



//[Test]
//public void GetAllDetails_ReturnsListOfBooks_WhenBooksExist()
//{
//    //Act 
//    var result = _bookRepository.GetAllDetails();

//    // Assert
//    Assert.IsNotNull(result);
//    //Assert.AreEqual(_booksList.Count, result.Count);
//    //Assert.AreEqual(_booksList[0].Title, result[0].Title);
//}

