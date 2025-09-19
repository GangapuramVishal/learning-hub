using BookInventorty.DAL.Data;
using BookInventorty.DAL.Models;
using BookInventorty.DAL.Repositorys;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query.Internal;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookInventoryTesting.RepositoryTestingUsingSQL
{
    [TestFixture]
    public class GetCallTests
    {
        public BookDbContext _bookDbContext;
        public BookRepository _bookRepository;
        public List<Book> _booksList;

        [SetUp]
        public void setup()
        {
            var configuration = new ConfigurationBuilder()
                .AddJsonFile("appsettings.json") //path to connection string
                .Build();

            var connectionString = configuration.GetConnectionString("DefaultSQLConnection");

            if (string.IsNullOrEmpty(connectionString))
            {
                throw new InvalidOperationException("Connection string not found or empty.");
            }

            // Create DbContext instance using the connection string
            var optionsBuilder = new DbContextOptionsBuilder<BookDbContext>();
            optionsBuilder.UseSqlServer(connectionString); // Use the appropriate database provider

            _bookDbContext = new BookDbContext(optionsBuilder.Options);

            _bookRepository = new BookRepository(_bookDbContext);
        }

        [SetUp]
        public void SetListOfBooks()
        {
            _booksList = new List<Book>()
            {
                new Book() { Id = 1, Title = "The Great Gatsby", Author = "F. Scott Fitzgerald", Genre = "Fiction", Quantity = 10 },
                new Book() { Id = 2, Title = "To Kill a Mockingbird", Author = "Harper Lee", Genre = "Fiction", Quantity = 8 },
                new Book() { Id = 3, Title = "1984", Author = "George Orwell", Genre = "Science Fiction", Quantity = 12 },
                new Book() { Id = 4, Title = "Pride and Prejudice", Author = "Jane Austen", Genre = "Romance", Quantity = 15 }
            };
        }

        [Test]
        public void GetBookById_ExistingId_ReturnsBook()
        {
            //Act
            int bookId = 1;
            Book result =  _bookRepository.GetByIdDetails(bookId);

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(bookId, result.Id);
        }

        [Test]
        public void GetAllDetails_ReturnsAllBooks()
        {
            // Act
            List<Book> result = _bookRepository.GetAllDetails();

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(4, result.Count);
        }

        //[Test]
        //public void AddBook_ValidBook_ReturnsSuccessMessage()
        //{
        //    //arrange
        //    var bookToAdd = new Book
        //    {
        //        //Id = 3,
        //        Title = "The Guide",
        //        Author = "R.K. Narayan",
        //        Genre = "Fiction",
        //        PublicationDate = DateTime.Now,
        //        Quantity = 10
        //    };

        //    //Act
        //    string result = _bookRepository.AddBook(bookToAdd);

        //    // Assert
        //    Assert.AreEqual("Book Inventory is Created", result);
        //}




        [TearDown]
        public void Teardown()
        {
            // Dispose _bookDbContext here
            _bookDbContext.Dispose();
        }
    }
}






