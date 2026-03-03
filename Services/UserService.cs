using BusinessObjects.Entities;
using Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _repo;
        public UserService(IUserRepository repo) => _repo = repo;
        //public IEnumerable<User> GetUsers() => _repo.GetUsers();
        public User? Login(string email, string password)
        {
            var user = _repo.FindByEmail(email);
            if (user is not null && user.PasswordHash == password) return user;
            return null;
        }
    }
}
