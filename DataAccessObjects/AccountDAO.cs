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

        // ===== GET ALL USER + ROLE =====
        public List<User> GetUsersExceptAdmin()
        {
            return _ctx.Users
                 .Include(u => u.Role)
                  .Where(u => u.Role.RoleName != "Admin")
                  .ToList();
        }

        // ===== GET ALL BASIC =====
        public IEnumerable<User> GetAll()
            => _ctx.Users.AsNoTracking().ToList();

        // ===== FIND BY EMAIL =====
        public User? FindByEmail(string email)
            => _ctx.Users.FirstOrDefault(c => c.Email == email);

        // ===== GET BY ID =====
        public User? GetById(int id)
        {
            return _ctx.Users
                .Include(u => u.UserRoles)
                .ThenInclude(ur => ur.Role)
                .FirstOrDefault(x => x.UserId == id);
        }

        // ===== ADD USER =====
        public void Add(User user)
        {
            _ctx.Users.Add(user);
            _ctx.SaveChanges();
        }

        // ===== DELETE USER =====
        public void Delete(int id)
        {
            var user = _ctx.Users.Find(id);
            if (user != null)
            {
                _ctx.Users.Remove(user);
                _ctx.SaveChanges();
            }
        }

        // ===== ASSIGN ROLE =====
        public void AssignRole(int userId, int roleId)
        {
            var role = _ctx.Roles.Find(roleId);
            if (role == null) return;

            role.RoleId = roleId;
            // ❌ Không cho gán Admin
            if (role == null || role.RoleName == "Admin") return;

            var exists = _ctx.UserRoles
                .Any(x => x.UserId == userId && x.RoleId == roleId);

            if (!exists)
            {
                _ctx.UserRoles.Add(new UserRole
                {
                    UserId = userId,
                    RoleId = roleId
                });

                _ctx.SaveChanges();
            }
        }

        public List<Role> GetRoles()
        {
            return _ctx.Roles
                .Where(r => r.RoleName != "Admin") // không cho chọn Admin
                .ToList();
        }
    }
}
