using LibraryManagementSystem.DAL.Entities;
using LibraryManagementSystem.DAL.Repositoties;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibraryManagementSystem.BLL.Services
{
    public class LoanService
    {
        private readonly LoanRepository _repo;

        public LoanService(LoanRepository repo)
        {
            _repo = repo;
        }

        public List<Loan> GetAllLoans()
        {
            return _repo.GetAll();
        }

        public void CreateLoan(Loan loan)
        {
            loan.LoanDate = DateTime.Now;          
            _repo.Add(loan);
        }

        public void ReturnBook(int loanId)
        {
            var loan = _repo.GetById(loanId);
            if (loan != null)
            {
                loan.ReturnDate = DateTime.Now;
                _repo.Update(loan);
            }
        }
    }
}
