using BusinessObjects.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services
{
    public interface IBookService
    {
        IEnumerable<Book> GetBooks();
        Book? GetBookById(int id);
        void SaveBook(Book p);
        void UpdateBook(Book p);
        void DeleteBook(Book p);
        void AddBookCopy(BookCopy copy);
        void DeleteBookCopy(BookCopy copy);
    }
}
