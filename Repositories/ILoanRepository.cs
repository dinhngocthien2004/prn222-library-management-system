using BusinessObjects.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repositories
{
    public interface ILoanRepository
    {
        List<Loan> GetAll();
        Loan? GetById(int id);
        void Add(Loan loan);
        void Update(Loan loan);
        void Delete(int id);
        void Save();
    }
}
