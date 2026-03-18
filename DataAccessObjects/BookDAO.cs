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
                .ToList();
        }

        public Book? GetById(int id)
        {
            return _ctx.Books
                .AsNoTracking()
                .Include(b => b.Category)
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
    }

}
