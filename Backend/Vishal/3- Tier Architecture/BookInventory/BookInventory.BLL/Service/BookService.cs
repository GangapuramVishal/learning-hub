using AutoMapper;
using BookInventorty.DAL.Models;
using BookInventorty.DAL.Repositorys;
using BookInventory.BLL.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookInventory.BLL.Service
{
    public class BookService : IBookService
    {
        private readonly IBookRepository _bookRepository;
        private readonly IMapper _mapper;

        public BookService(IBookRepository bookRepository, IMapper mapper)
        {
            _bookRepository = bookRepository;
            _mapper = mapper;
        }
        public List<Book> GetAllBooks()
        {
            return _bookRepository.GetAllDetails().ToList();
        }
        public Book GetBookById(int id)
        {
            return _bookRepository.GetByIdDetails(id);
        }

        public string CreateBook(CreateDTO bookDTO)
        {
            try
            {
                var book = _mapper.Map<Book>(bookDTO);
                _bookRepository.AddBook(book);
                return "Book created successfully";
            }
            catch (Exception ex)
            {
                return $"Error creating book: {ex.Message}";
            }
        }

        public string UpdateBook(int id, UpdateDTO bookDTO)
        {
            try
            {
                if (bookDTO.Id == null)
                {
                    return "Book not found";
                }
                var updatedBook = _mapper.Map<Book>(bookDTO);
                _bookRepository.UpdateBookRepo(updatedBook, id);
                return "Book updated successfully";
            }
            catch (Exception ex)
            {
                return $"Error updating book: {ex.Message}";
            }
        }

        public void UpdateBookPartial(UpdateDTO bookDTO, int id)
        {
            var existingBook = _bookRepository.GetByIdDetails(id);
            if (existingBook != null)
            {
                
                _mapper.Map(bookDTO, existingBook);
                _bookRepository.UpdateBookRepo(existingBook, id);
            }
        }

        public bool DeleteBook(Book result)
        {
            _bookRepository.DeleteById(result);
            return true;
        }


    }
}
