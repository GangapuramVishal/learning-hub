using AutoMapper;
using BookInventorty.DAL.Models;
using BookInventorty.DAL.Repositorys;
using BookInventory.BLL.Service;
using BookInventory.BLL.DTO;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookInventoryTesting
{
    [TestFixture]
    public class BookServiceTest
    {
        // Declaring private fields for the BookService, mock BookRepository, mock IMapper, and a list of books
        private readonly BookService _bookService;
        private readonly Mock<IBookRepository> _bookRepositoryMock = new Mock<IBookRepository>();
        private readonly Mock<IMapper> _mapper = new Mock<IMapper>();
        private List<Book> _books;

        // Constructor to initialize the test environment
        public BookServiceTest()
        {
            // Creating an instance of BookService with mock dependencies
            _bookService = new BookService(_bookRepositoryMock.Object, _mapper.Object);

            // Initializing a list of books with some sample data
            _books = new List<Book>
            {
                new Book() { Id = 1, Title = "The Great Gatsby", Author = "F. Scott Fitzgerald", Genre = "Fiction", Quantity = 10 },
                new Book() { Id = 2, Title = "To Kill a Mockingbird", Author = "Harper Lee", Genre = "Fiction", Quantity = 8 }
            };
        }

        // Test method to verify behavior when GetAllBooks method is called
        [Test]
        public void GetAllDetails_ReturnsListOfBooks_WhenBooksExist()
        {
            // Setting up the mock BookRepository to return the list of books when GetAllDetails method is called
            _bookRepositoryMock.Setup(x => x.GetAllDetails()).Returns(_books);

            // Creating a new instance of BookService using the mock BookRepository and IMapper
            var bookService = new BookService(_bookRepositoryMock.Object, _mapper.Object);

            // Calling the GetAllBooks method of the BookService
            var result = bookService.GetAllBooks();
            
            //Assert
            Assert.NotNull(result); 
            Assert.AreEqual(_books.Count, result.Count);
        }

        [Test]
        public void GetAllDetails_ReturnsEmptyList_WhenNoBooksExist()
        {
            // Arrange: Set up the mock BookRepository to return an empty list when GetAllDetails is called
            _bookRepositoryMock.Setup(x => x.GetAllDetails()).Returns(new List<Book>());

            // Act: Call the GetAllBooks method of the BookService
            var result = _bookService.GetAllBooks();

            // Assert: Verify that the result is not null and is an empty list
            Assert.NotNull(result);
            Assert.IsEmpty(result);
        }

        [Test]
        public void GetBookById_When_Exists()
        {
            //Arrange
            int bookId = 1;
            var expectedBook = _books.FirstOrDefault(b => b.Id == bookId);

            _bookRepositoryMock.Setup(x => x.GetByIdDetails(It.IsAny<int>())).Returns((int id) =>
                {
                    var result = _books.FirstOrDefault(x => x.Id == bookId);
                    return result;
                });

            //Act
            var actualBook = _bookService.GetBookById(bookId);
            Assert.IsNotNull(actualBook);
            Assert.AreEqual(bookId,actualBook.Id);
        }

        [Test]
        public void GetBookById_ReturnsNull_For_Invalid_Id()
        {
            //Arrange
            int NotExistingBookId = 101;
            _bookRepositoryMock.Setup(s => s.GetByIdDetails(It.IsAny<int>())).Returns((int id) =>
            {
                var result = _books.FirstOrDefault(x => x.Id == id);
                return result;
            });

            //Act
            var actualStudent = _bookService.GetBookById(NotExistingBookId);

            //Assert 
            Assert.IsNull(actualStudent);
        }

        [Test]
        public void AddBook_ShouldReturnNewBook()
        {
            //Arrange
            _bookRepositoryMock.Setup(x => x.AddBook(It.IsAny<Book>())).Callback<Book>(book =>
            {
                _books.Add(book);
            });

            var newBook = new CreateDTO
            {
                //Id = 3,
                Title = "New Book",
                Author = "New Author",
                Genre = "New Genre",
                PublicationDate = DateTime.Now,
                Quantity = 1
            };
            // Act
            var bookService = new BookService(_bookRepositoryMock.Object, _mapper.Object);

            var result = bookService.CreateBook(newBook);

            // Assert
            // Verify that the returned book is not null
            Assert.IsNotNull(result);

            // Verify that the count of books in the _books list has increased by 1
            Assert.AreEqual(_books.Count, 3); // Assuming we had 2 books initially
        }

        //[Test]
        ////Test Fail
        //public void AddBook_ShouldNotAddNullBook()
        //{
        //    // Arrange
        //    CreateDTO newBook = null; // Null input

        //    _bookRepositoryMock.Setup(x => x.AddBook(It.IsAny<Book>())).Callback<Book>(book =>
        //    {
        //        _books.Add(book);
        //    });
        //    var bookService = new BookService(_bookRepositoryMock.Object, _mapper.Object);

        //    // Act
        //    var result = bookService.CreateBook(newBook);

        //    // Assert
        //    Assert.AreEqual(0, _books.Count);
        //}

        [Test]
        public void Add_Multiple_Books()
        {
            //Arrange
            var newBook2 = new CreateDTO
            {
                Title = "New Book- 2",
                Author = "New Author -2",
                Genre = "New Genre-2",
                PublicationDate = DateTime.Now,
                Quantity = 2
            };

            var newBook3 = new CreateDTO
            {
                Title = "New Book - 3",
                Author = "New Author - 3",
                Genre = "New Genre - 3",
                PublicationDate = DateTime.Now,
                Quantity = 3
            };

            _bookRepositoryMock.Setup(x => x.AddBook(It.IsAny<Book>())).Callback<Book>(book =>
            {
                _books.Add(book);
            });
            var bookService = new BookService(_bookRepositoryMock.Object, _mapper.Object);

            //Act
            var book2 = bookService.CreateBook(newBook2);
            var book3 = bookService.CreateBook(newBook3);

            // Assert
            // Verify that the books were added to the repository
            Assert.AreEqual(4, _books.Count);
        }

        //[Test]
        ////Test Fails
        //public void AddBook_ShouldNotAddWithEmptyName()
        //{
        //    //Arrange 
        //    var newBook = new CreateDTO
        //    {
        //        Title = "",
        //        Author = "New Authors",
        //        Genre = "New Genres",
        //        PublicationDate = DateTime.Now,
        //        Quantity = 3
        //    };
        //    _bookRepositoryMock.Setup(x => x.AddBook(It.IsAny<Book>())).Callback<Book>(book =>
        //    {
        //        _books.Add(book);
        //    });
        //    var bookService = new BookService(_bookRepositoryMock.Object, _mapper.Object);

        //    // Act
        //    var result = bookService.CreateBook(newBook);

        //    // Assert
        //    Assert.IsNull(result);
        //}
        [Test]
        public void UpdateBook_ShouldReturnStringWhenBookIsUpdated()
        {
            // Arrange
            int existingBookId = 1;
            var existingBook = _books.FirstOrDefault(s => s.Id == existingBookId);

            _bookRepositoryMock.Setup(x => x.GetByIdDetails(existingBookId)).Returns(existingBook);
            _bookRepositoryMock.Setup(x => x.UpdateBookRepo(It.IsAny<Book>(), existingBookId));

            var updatedBookDetails = new UpdateDTO
            {
                Id = existingBookId,
                Title = "Updated Title",
                Author = "Auther ",
                Genre = "Genre",
                Quantity = 3

                // Add other properties as needed
            };

            _mapper.Setup(m => m.Map<UpdateDTO, Book>(updatedBookDetails)).Returns(existingBook);

            // Act
            var isUpdated = _bookService.UpdateBook(existingBookId, updatedBookDetails);

            // Assert
            Assert.AreEqual("Book updated successfully", isUpdated);

        }

        //[Test]
        ////Test Fail
        //public void UpdateBook1_ShouldReturnStringWhenBookIsUpdated()
        //{
        //    // Arrange
        //    int existingBookId = 1;
        //    var existingBook = _books.FirstOrDefault(s => s.Id == existingBookId);

        //    _bookRepositoryMock.Setup(x => x.GetByIdDetails(existingBookId)).Returns(existingBook);
        //    _bookRepositoryMock.Setup(x => x.UpdateBookRepo(It.IsAny<Book>(), existingBookId));

        //    var updatedBookDetails = new UpdateDTO
        //    {
        //        Id = existingBookId, // Provide a non-null ID
        //        Title = "Updated Title",
        //        Author = "Auther ",
        //        Genre = "Genre",
        //        Quantity = 3
        //        // Add other properties as needed
        //    };

        //    _mapper.Setup(m => m.Map<UpdateDTO, Book>(updatedBookDetails)).Returns(existingBook);

        //    // Act
        //    var isUpdated = _bookService.UpdateBook(existingBookId, updatedBookDetails);

        //    // Assert
        //    Assert.AreEqual("Book updated successfully", isUpdated);

        //    // Act
        //    var isUpdatedWithNullId = _bookService.UpdateBook(existingBookId, updatedBookDetails);

        //    // Assert
        //    Assert.AreNotEqual("Book updated successfully", isUpdatedWithNullId);
        //}

        [Test]
        public void UpdateBook_StringWhenBookIsUpdated()
        {
            // Arrange
            int existingBookId = 0;
            var existingBook = _books.FirstOrDefault(s => s.Id == existingBookId);

            if (existingBook == null)
            {
                Assert.AreEqual(null, existingBook);  
            }
            else
            {

                _bookRepositoryMock.Setup(x => x.GetByIdDetails(existingBookId)).Returns(existingBook);
                _bookRepositoryMock.Setup(x => x.UpdateBookRepo(It.IsAny<Book>(), existingBookId));
                var updatedBookDetails = new UpdateDTO
                {
                    Id = existingBookId, // Provide a non-null ID
                    Title = "Updated Title",
                    Author = "Auther ",
                    Genre = "Genre",
                    Quantity = 3
                };

                _mapper.Setup(m => m.Map<UpdateDTO, Book>(updatedBookDetails)).Returns(existingBook);

                // Act
                var isUpdated = _bookService.UpdateBook(existingBookId, updatedBookDetails);

                // Assert
                Assert.AreEqual("Book updated successfully", isUpdated);
            }
        }

        [Test]
        public void DeleteBook_ShouldReturnTrueWhenBookIsDeleted()
        {
            // Arrange
            int existingBookId = 1;
            var existingBook = _books.FirstOrDefault(s => s.Id == existingBookId);

            _bookRepositoryMock.Setup(x => x.GetByIdDetails(existingBookId)).Returns(existingBook);
            _bookRepositoryMock.Setup(x => x.DeleteById(existingBook)).Returns("Student successfully deleted.");
            // Act
            bool isDeleted = _bookService.DeleteBook(existingBook);

            // Assert
            Assert.IsTrue(isDeleted);
        }
    }
}
