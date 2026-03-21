using BusinessObjects.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services
{
    public interface IBookCopyService
    {
        List<BookCopy> GetBookCopies(); // 🔥 thêm
        List<BookCopy> GetAvailableCopies(int bookId);
        void Update(BookCopy copy);
        void AddBookCopy(BookCopy copy);
    }
}
