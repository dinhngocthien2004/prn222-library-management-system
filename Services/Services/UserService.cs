using LibraryManagementSystem.DAL.Entities;
using LibraryManagementSystem.DAL.Repositoties;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibraryManagementSystem.BLL.Services
{
    public class UserService
    {
        private UserRepository _repo = new(); 

        public User? Authenticate(string email)
        {
            return _repo.FindByEmail(email);
        }
        public User? Authenticate(string email, string pass)
        {
            return _repo.FindByEmailAndPassword(email, pass);
        }

    }
}
