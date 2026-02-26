using LibraryManagementSystem.DAL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibraryManagementSystem.DAL.Repositoties
{
    public class UserRepository
    {
        private LibraryManagementDbContext _ctx; 
     
        public User? FindByEmail(string email)
        {
            
            return _ctx.Users.FirstOrDefault(nt => nt.Email == email);

        }

        public User? FindByEmailAndPassword(string email, string pass)
        {
            //VIẾT WHERE TRÊN CẢ EMAIL VÀ PASS
            return _ctx.Users.FirstOrDefault(nt => nt.Email == email && nt.PasswordHash == pass);
        }

    }
}
