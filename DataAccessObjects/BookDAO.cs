using BusinessObjects.Entities;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;

namespace DataAccessObjects
{
    public class BookDAO
    {
        private readonly LibraryManagementDbContext _ctx;

        public BookDAO(LibraryManagementDbContext ctx) => _ctx = ctx;

        public IEnumerable<Book> GetAll()
            => _ctx.Books.AsNoTracking().Include(p => p.Categories);

        public Book? GetById(int id)
            => _ctx.Books.AsNoTracking()
                         .Include(p => p.Categories)
                         .FirstOrDefault(p => p.BookId == id);

        public void Create(Book p)
        {
            _ctx.Books.Add(p);
            _ctx.SaveChanges();
        }

        public void Update(Book p)
        {
            _ctx.Entry(p).State = EntityState.Modified;
            _ctx.SaveChanges();
        }

        public void Delete(Book p)
        {
            _ctx.Books.Remove(p);
            _ctx.SaveChanges();
        }
    }
}