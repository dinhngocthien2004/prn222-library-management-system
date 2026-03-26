using BusinessObjects.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services
{
    public interface IUserService
    {
        IEnumerable<User> GetUsers();
        User? Login(string email, string password);

        List<User> GetUsersExceptAdmin();
        List<Role> GetRoles();
        void CreateUser(User user);
        void DeleteUser(int id);
        void AssignRole(int userId, int roleId);
    }
}
