using BusinessObjects.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessObjects
{
    public class AccountDAO
    {
        private readonly LibraryManagementDbContext _ctx;
        public AccountDAO(LibraryManagementDbContext ctx) => _ctx = ctx;
        public IEnumerable<User> GetAll() => _ctx.Users.AsNoTracking().ToList();
        public User? FindByEmail(string email) =>
            _ctx.Users.AsNoTracking().FirstOrDefault(c => c.Email == email);
    }
}
