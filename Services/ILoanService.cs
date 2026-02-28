using BusinessObjects.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services
{
    public interface ILoanService
    {
        List<Loan> GetAllLoans();
        Loan? GetLoanById(int id);
        void CreateLoan(Loan loan);
        void UpdateLoan(Loan loan);
        void DeleteLoan(int id);
    }
}
