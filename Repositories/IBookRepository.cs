using BusinessObjects.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repositories
{
    public interface IBookRepository
    {
        IEnumerable<Book> GetBooks();
        Book? GetBookById(int id);
        void SaveBook(Book p);
        void UpdateBook(Book p);
        void DeleteBook(Book p);
    }
}
