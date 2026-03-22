using BusinessObjects.Entities;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;

namespace DataAccessObjects
{
    public class BookDAO
    {
        private readonly LibraryManagementDbContext _ctx;

        public BookDAO(LibraryManagementDbContext ctx)
        {
            _ctx = ctx;
        }

        public IEnumerable<Book> GetAll()
        {
            return _ctx.Books
                .AsNoTracking()
                .Include(b => b.Category)
                .Include(b => b.BookCopies) // 🔥 THÊM DÒNG NÀY
                .ToList();
        }

        public Book? GetById(int id)
        {
            return _ctx.Books
                .AsNoTracking()
                .Include(b => b.Category)
                .Include(b => b.BookCopies) // 🔥 thêm luôn cho chắc
                .FirstOrDefault(b => b.BookId == id);
        }

        public void Create(Book book)
        {
            _ctx.Books.Add(book);
            _ctx.SaveChanges();
        }

        public void Update(Book book)
        {
            _ctx.Books.Update(book);
            _ctx.SaveChanges();
        }

        public void Delete(Book book)
        {
            _ctx.Books.Remove(book);
            _ctx.SaveChanges();
        }

        public void AddBookCopy(BookCopy copy)
        {
            _ctx.BookCopies.Add(copy);
            _ctx.SaveChanges();
        }

        public void DeleteBookCopy(BookCopy copy)
        {
            _ctx.BookCopies.Remove(copy);
            _ctx.SaveChanges();
        }
        public void BorrowBook(int bookId)
        {
            var copy = _ctx.BookCopies
                .FirstOrDefault(c => c.BookId == bookId && c.IsAvailable);

            if (copy == null)
                throw new Exception("Hết sách");

            copy.IsAvailable = false;

            _ctx.SaveChanges(); // 🔥 bắt buộc
        }
    }
}