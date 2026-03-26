using BusinessObjects.Entities;
using Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BCrypt.Net;

namespace Services
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _repo;
        public UserService(IUserRepository repo) => _repo = repo;
        public IEnumerable<User> GetUsers() => _repo.GetUsers();
        public User? Login(string email, string password)
        {
            var user = _repo.FindByEmail(email);
            if (user is not null && user.PasswordHash == password) return user;
            return null;
        }
        public void CreateUser(User user)
        {
            user.PasswordHash = BCrypt.Net.BCrypt.HashPassword(user.PasswordHash);
          // user.RoleId = 2; // Member (phải tồn tại trong DB)
            _repo.Add(user);
        }

        public void DeleteUser(int id) => _repo.Delete(id);

        public void AssignRole(int userId, int roleId)
            => _repo.AssignRole(userId, roleId);
        public List<User> GetUsersExceptAdmin()
        {
            return _repo.GetUsersExceptAdmin();
        }
        public List<Role> GetRoles()
        {
            return _repo.GetRoles();
        }
    }
}
