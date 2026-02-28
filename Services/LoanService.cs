using BusinessObjects.Entities;
using Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services
{
    public class LoanService : ILoanService
    {
        private readonly ILoanRepository _loanRepository;

        public LoanService(ILoanRepository loanRepository)
        {
            _loanRepository = loanRepository;
        }

        public List<Loan> GetAllLoans()
        {
            return _loanRepository.GetAll();
        }

        public Loan? GetLoanById(int id)
        {
            return _loanRepository.GetById(id);
        }

        public void CreateLoan(Loan loan)
        {
            loan.LoanDate = DateTime.Now;
            loan.Status = 1; // 1 = Borrowing

            _loanRepository.Add(loan);
            _loanRepository.Save();
        }

        public void UpdateLoan(Loan loan)
        {
            _loanRepository.Update(loan);
            _loanRepository.Save();
        }

        public void DeleteLoan(int id)
        {
            _loanRepository.Delete(id);
            _loanRepository.Save();
        }
    }
}
