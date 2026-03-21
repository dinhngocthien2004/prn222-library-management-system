using BusinessObjects.Entities;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;

namespace DataAccessObjects
{
    public class BookCopyDAO
    {
        private readonly LibraryManagementDbContext _ctx;

        public BookCopyDAO(LibraryManagementDbContext ctx)
        {
            _ctx = ctx;
        }

        // ✅ Lấy tất cả
        public List<BookCopy> GetAll()
        {
            return _ctx.BookCopies.ToList();
        }

        // ✅ Lấy theo ID
        public BookCopy? GetById(int id)
        {
            return _ctx.BookCopies.Find(id);
        }

        // ✅ Update
        public void Update(BookCopy copy)
        {
            _ctx.BookCopies.Update(copy);
            _ctx.SaveChanges();
        }

        public void AddBookCopy(BookCopy copy)
        {
            _ctx.BookCopies.Add(copy);
            _ctx.SaveChanges();
        }
    }
}