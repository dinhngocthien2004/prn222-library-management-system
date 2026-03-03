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
        //IEnumerable<User> GetUsers();
        User? FindByEmail(string email);
    }
}
