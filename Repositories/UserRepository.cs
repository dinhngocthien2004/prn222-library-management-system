using BusinessObjects.Entities;
using DataAccessObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly UserDAO _dao;
        public UserRepository(UserDAO dao) => _dao = dao;
        public IEnumerable<User> GetUsers() => _dao.GetAll();
        public User? FindByEmail(string email) => _dao.FindByEmail(email);

       
    }
}
