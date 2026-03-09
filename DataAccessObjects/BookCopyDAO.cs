using BusinessObjects.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessObjects
{
    public class BookCopyDAO
    {
        private readonly LibraryManagementDbContext _ctx;
        public BookCopyDAO(LibraryManagementDbContext ctx) => _ctx = ctx;

        public IEnumerable<BookCopy> GetAll() => _ctx.BookCopies.AsNoTracking().ToList();
        public BookCopy? GetById(int id) => _ctx.BookCopies.Find(id);
    }
}
