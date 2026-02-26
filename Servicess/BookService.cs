using BusinessObjects.Entities;
using Repositoriess;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Servicess
{
    public class BookService : IBookService
    {
        private readonly IBookRepository _repo;
        public BookService(IBookRepository repo) => _repo = repo;

        public IEnumerable<Book> GetBooks() => _repo.GetBooks();
        public Book? GetBookById(int id) => _repo.GetBookById(id);
        public void SaveBook(Book p) => _repo.SaveBook(p);
        public void UpdateBook(Book p) => _repo.UpdateBook(p);
        public void DeleteBook(Book p) => _repo.DeleteBook(p);
    }
}
