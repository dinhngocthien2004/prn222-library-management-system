using LibraryManagementSystem.DAL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibraryManagementSystem.DAL.Repositoties
{
    public class BookRepository
    {
        private readonly LibraryManagementDbContext _context;

        public BookRepository(LibraryManagementDbContext context)
        {
            _context = context;
        }

        public List<Book> GetAllBooks()
        {
            // Lấy danh sách sách và include cả tên tác giả
            return _context.Books.ToList();
        }
    }
}
