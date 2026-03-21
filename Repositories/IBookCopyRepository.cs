using BusinessObjects.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repositories
{
    public interface IBookCopyRepository
    {
        IEnumerable<BookCopy> GetBookCopies();
        BookCopy? GetBookCopyById(int id);
        List<BookCopy> GetAll();   // 🔥 thêm
        void Update(BookCopy copy); // 🔥 thêm
        void AddBookCopy(BookCopy copy);
    }
}
