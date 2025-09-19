using BookInventorty.DAL.Data;
using BookInventorty.DAL.Models;

namespace BookInventorty.DAL.Repositorys
{
    public class BookRepository : IBookRepository
    {
        private readonly BookDbContext _bookDbContext;
        public BookRepository(BookDbContext bookDbContext)
        {
            _bookDbContext = bookDbContext;
        }
        public List<Book> GetAllDetails()
        {
            return _bookDbContext.Books.ToList();
        }

        public Book GetByIdDetails(int id)
        {
            return _bookDbContext.Books.FirstOrDefault(b => b.Id == id);   // It uses a lambda expression (b => b.Id == id) to specify the condition for finding
                                                                           // the book by its ID. The FirstOrDefault method returns the first element of the sequence                                                                   // that satisfies the condition or null if no such element is found.
        }

        public string AddBook(Book book)
        {
            try
            {
                _bookDbContext.Add(book);
                _bookDbContext.SaveChanges();
                return "Book Inventory is Created";
            }
            catch (Exception ex)
            {
                return $"Error adding book: {ex.Message}";
            }
        }
        public string UpdateBookRepo(Book updatedBook, int id)
        {
            // Find the book with the specified ID in the database
            var existingBook = _bookDbContext.Books.FirstOrDefault(u => u.Id == id);
            if (existingBook != null)
            {
                //var mapper = _mapper.Map<updatedbook, Book>(updatedbook, result);
                //existingBook.Id = updatedBook.Id;
                existingBook.Title = updatedBook.Title;
                existingBook.Author = updatedBook.Author;
                existingBook.Genre = updatedBook.Genre;
                //existingBook.PublicationDate = updatedBook.PublicationDate;
                existingBook.Quantity = updatedBook.Quantity;

                _bookDbContext.Books.Update(existingBook);
                // Save the changes to the database
                _bookDbContext.SaveChanges();
                return "Book updated succcessfully";
            }
            else
            {
                return "Book not found";
            }

        }
        public void PatchBook(Book patchedBook)
        {
            _bookDbContext.Update(patchedBook);
            _bookDbContext.SaveChanges();
        }

        public string DeleteById(Book book)
        {
            _bookDbContext.Remove(book);
            _bookDbContext.SaveChanges();
            return "Book deleted successfully";
        }
    }
}
