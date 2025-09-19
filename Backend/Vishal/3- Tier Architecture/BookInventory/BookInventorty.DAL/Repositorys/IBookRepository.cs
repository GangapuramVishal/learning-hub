using BookInventorty.DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookInventorty.DAL.Repositorys
{
    public interface IBookRepository
    {
        public List<Book> GetAllDetails();
        public Book GetByIdDetails(int id);
        public string AddBook(Book book);
        public string DeleteById(Book book);
        string UpdateBookRepo(Book result, int id);
        public void PatchBook(Book patchedBook);
        
    }
}
