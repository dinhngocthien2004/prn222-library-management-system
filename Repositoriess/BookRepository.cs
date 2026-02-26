using BusinessObjects.Entities;
using DataAccessObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repositoriess
{
    public class BookRepository : IBookRepository
    {
        private readonly BookDAO _dao;
        public BookRepository(BookDAO dao) => _dao = dao;

        public IEnumerable<Book> GetBooks() => _dao.GetAll();
        public Book? GetBookById(int id) => _dao.GetById(id);
        public void SaveBook(Book p) => _dao.Create(p);
        public void UpdateBook(Book p) => _dao.Update(p);
        public void DeleteBook(Book p) => _dao.Delete(p);
    }
}
