using AutoMapper;
using BookInventorty.DAL.Models;
using BookInventory.BLL.DTO;
using BookInventory.BLL.Service;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace BookInventory.API.Controllers
{
    [ApiController]
    [Route("api/books")]
    public class BookController : ControllerBase
    {
        private readonly IBookService _bookService;
        private readonly IMapper _mapper;
        private readonly ILogger<BookController> _logger;  // Add ILogger


        public BookController(IBookService bookService, IMapper mapper, ILogger<BookController> logger)
        {
            _bookService = bookService;
            _mapper = mapper;
            _logger = logger;         // Assign logger

        }

        [HttpGet]
        public IActionResult GetBooks()
        {
            try
            {
                _logger.LogInformation("Getting all books"); // Log information

                List<Book> books = _bookService.GetAllBooks();
                
                return Ok(books);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while getting all books"); // Log error with exception

                return BadRequest(ex.Message);
            }
        }

        [HttpGet("{id:int}", Name = "GetBooks")]
        public IActionResult GetByIdDetails(int id)
        {
            try
            {
                _logger.LogInformation("Getting book by ID: {Id}", id); // Log information with parameter

                var book = _bookService.GetBookById(id);
                if (book == null)
                {
                    return NotFound($"Book with ID {id} not found"); // Return 404 Not Found with a message
                }
                return Ok(book);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while retrieving the book with ID: {Id}", id); // Log error with exception and parameter

                return BadRequest($"An error occurred while retrieving the book: {ex.Message}");
            }
        }

        [HttpPost("creating-a-book")]
        public IActionResult CreateABook([FromBody] CreateDTO book)
        {
            try
            {
                _logger.LogInformation("Attempting to create a book"); // Log information

                var result = _bookService.CreateBook(book);

                return Ok("Book created successfully");
            }

            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while creating a book: {ErrorMessage}", ex.Message); // Log error with exception and message

                return BadRequest(ex.Message);
            }
        }


        [HttpPut("update-book/{id}")]
        public IActionResult UpdateBookDetails(int id, [FromBody] UpdateDTO updateDTO)
        {
            try
            {
                _logger.LogInformation("Attempting to update book details for ID: {BookId}", id); // Log information

                // Check if the input DTO is null or if its ID doesn't match the provided ID
                if (updateDTO == null || updateDTO.Id != id)
                {
                    _logger.LogError("Invalid input: UpdateDTO is null or ID doesn't match provided ID");

                    return BadRequest("Invalid input");
                }

                // Check if the book with the provided ID exists in the database
                var existingBook = _bookService.GetBookById(id);

                if (existingBook == null)
                {
                    _logger.LogWarning("Book with ID {BookId} not found", id); // Log warning

                    return NotFound($"Book with ID {id} not found");
                }

                // Update the book details
                _bookService.UpdateBook(id, updateDTO);

                _logger.LogInformation("Book details updated successfully"); // Log information

                // Return the updated book details
                var updatedBook = _bookService.GetBookById(id);
                return Ok(updatedBook);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while updating the book: {ErrorMessage}", ex.Message); // Log error with exception and message

                return BadRequest($"An error occurred while updating the book: {ex.Message}");
            }
        }

        [HttpDelete("delete-book/{id}")]
        public IActionResult DeleteBook(int id)
        {
            try
            {
                _logger.LogInformation("Attempting to delete book with ID: {BookId}", id); // Log information

                var result = _bookService.GetBookById(id);
                _bookService.DeleteBook(result);
                return Ok("Book Deleted");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while deleting the book: {ErrorMessage}", ex.Message); // Log error with exception and message

                return BadRequest(ex.Message);
            }
        }
    }

}
