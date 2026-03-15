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
        IEnumerable<Loan> GetLoans();
        Loan? GetLoanById(int id);
        void SaveLoan(Loan p);
        void UpdateLoan(Loan p);
        void DeleteLoan(Loan p);
        List<Loan> GetHistory();
    }
}
