using BookInventorty.DAL.Data;
using BookInventorty.DAL.Models;
using BookInventorty.DAL.Repositorys;
using Microsoft.EntityFrameworkCore;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookInventoryTesting
{
    public class Test
    {
    //    [Test]
    //    public void GetAllDetails_ReturnsAllBooks()
    //    {
    //        // Arrange
    //        var books = new List<Book>
    //{
    //    new Book { Id = 1, Title = "Book 1" },
    //    new Book { Id = 2, Title = "Book 2" }
    //};

    //        var mockDbContext = new Mock<BookDbContext>();
    //        mockDbContext.Setup(m => m.Books).Returns(DbSetMock(books));

    //        var repository = new BookRepository(mockDbContext.Object);

    //        // Act
    //        var result = repository.GetAllDetails();

    //        // Assert
    //        Assert.AreEqual(2, result.Count);
    //        Assert.AreEqual("Book 1", result[0].Title);
    //        Assert.AreEqual("Book 2", result[1].Title);
    //    }

        // Helper method to create a DbSet mock from a list of items
        //public static DbSet<T> DbSetMock<T>(IEnumerable<T> data) where T : class
        //{
        //    var queryableData = data.AsQueryable();

        //    var mockSet = new Mock<DbSet<T>>();
        //    mockSet.As<IQueryable<T>>().Setup(m => m.Provider).Returns(queryableData.Provider);
        //    mockSet.As<IQueryable<T>>().Setup(m => m.Expression).Returns(queryableData.Expression);
        //    mockSet.As<IQueryable<T>>().Setup(m => m.ElementType).Returns(queryableData.ElementType);
        //    mockSet.As<IQueryable<T>>().Setup(m => m.GetEnumerator()).Returns(queryableData.GetEnumerator());

        //    return mockSet.Object;
        //}

        //[Test]
        //public void GetByIdDetails_ReturnsCorrectBook()
        //{
        //    // Arrange
        //    var mockDbContext = new Mock<BookDbContext>();
        //    var books = new List<Book>
        //    {
        //        new Book { Id = 1, Title = "Book 1" },
        //        new Book { Id = 2, Title = "Book 2" }
        //    };
        //    mockDbContext.Setup(m => m.Books).Returns(books.AsQueryable());

        //    var repository = new BookRepository(mockDbContext.Object);

        //    // Act
        //    var result = repository.GetByIdDetails(1);

        //    // Assert
        //    Assert.IsNotNull(result);
        //    Assert.AreEqual(1, result.Id);
        //    Assert.AreEqual("Book 1", result.Title);
        //}

        //[Test]
        //public void AddBook_CreatesNewBook()
        //{
        //    // Arrange
        //    var mockDbContext = new Mock<BookDbContext>();
        //    var repository = new BookRepository(mockDbContext.Object);
        //    var newBook = new Book { Id = 3, Title = "New Book" };

        //    // Act
        //    var result = repository.AddBook(newBook);

        //    // Assert
        //    Assert.AreEqual("Book Inventory is Created", result);
        //    mockDbContext.Verify(m => m.Add(It.IsAny<Book>()), Times.Once);
        //    mockDbContext.Verify(m => m.SaveChanges(), Times.Once);
        //}
    }
}
