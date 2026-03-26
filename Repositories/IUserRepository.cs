using BusinessObjects.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repositories
{
    public interface IUserRepository
    {
        public IEnumerable<User> GetUsers();
        User? FindByEmail(string email);

        List<User> GetUsersExceptAdmin();
        List<Role> GetRoles();

        User GetById(int id);
        void Add(User user);
        void Delete(int id);

        void AssignRole(int userId, int roleId);
    }
}
