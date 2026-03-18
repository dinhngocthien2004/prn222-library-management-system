using BusinessObjects.Entities;
using DataAccessObjects.Migrations;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;

namespace DataAccessObjects
{
    public class ReaderDAO
    {
        private readonly LibraryManagementDbContext _ctx;

        public ReaderDAO(LibraryManagementDbContext ctx)
        {
            _ctx = ctx;
        }

        public IEnumerable<Reader> GetAll()
        {
            return _ctx.Readers
            .Include(r => r.User)
            .ToList();
        }

        public Reader? GetById(int id)
        {
            return _ctx.Readers
                .Include(r => r.User)
                .FirstOrDefault(r => r.ReaderId == id);
        }

        public void Create(Reader r)
        {
            _ctx.Readers.Add(r);
            _ctx.SaveChanges();
        }

        public void Update(Reader r)
        {
            _ctx.Readers.Update(r);
            _ctx.SaveChanges();
        }

        public void Delete(Reader r)
        {
            _ctx.Readers.Remove(r);
            _ctx.SaveChanges();
        }
    }
}




