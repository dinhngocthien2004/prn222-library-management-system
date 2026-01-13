using LibraryManagementSystem.DAL.Entities;
using LibraryManagementSystem.DAL.Repositoties;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibraryManagementSystem.BLL.Services
{
    public class BookService
    {
        private readonly BookRepository _bookRepo;

        public BookService(BookRepository bookRepo)
        {
            _bookRepo = bookRepo;
        }

        public List<Book> GetBooksForDisplay()
        {
            // Tại đây có thể viết thêm code kiểm tra logic nếu cần
            return _bookRepo.GetAllBooks();
        }
    }
}
