using BusinessObjects.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessObjects
{
    public class CategoryDAO
    {
        private readonly LibraryManagementDbContext _ctx;
        public CategoryDAO(LibraryManagementDbContext ctx) => _ctx = ctx;

        public IEnumerable<Category> GetAll() => _ctx.Categories.AsNoTracking().ToList();
        public Category? GetById(int id) => _ctx.Categories.Find(id);
    }
}
