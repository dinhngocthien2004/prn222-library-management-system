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
        private readonly AccountDAO _dao;
        public UserRepository(AccountDAO dao) => _dao = dao;
        public IEnumerable<User> GetUsers() => _dao.GetAll();
        public User? FindByEmail(string email) => _dao.FindByEmail(email);

        
        public List<User> GetUsersExceptAdmin()
        {
            return _dao.GetUsersExceptAdmin();
        }

       
        public User GetById(int id) => _dao.GetById(id);

        public void Add(User user) => _dao.Add(user);

        public void Delete(int id) => _dao.Delete(id);

        public void AssignRole(int userId, int roleId)
        {
            _dao.AssignRole(userId, roleId);
        }
    }
}
