using BookInventorty.DAL.Models;
using BookInventory.BLL.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookInventory.BLL.Service
{
    public interface IBookService
    {
        
        public List<Book> GetAllBooks();
        public Book GetBookById(int id);
        public string CreateBook(CreateDTO bookDTO);
        public string UpdateBook(int id, UpdateDTO bookDTO);
        public bool DeleteBook(Book result);
        public void UpdateBookPartial(UpdateDTO bookDTO, int id);

    }
}
