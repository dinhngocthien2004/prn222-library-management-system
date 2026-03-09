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
        private readonly ILoanRepository _repo;
        public LoanService(ILoanRepository repo) => _repo = repo;

        public IEnumerable<Loan> GetLoans() => _repo.GetLoans();
        public Loan? GetLoanById(int id) => _repo.GetLoanById(id);
        public void SaveLoan(Loan p) => _repo.SaveLoan(p);
        public void UpdateLoan(Loan p) => _repo.UpdateLoan(p);
        public void DeleteLoan(Loan p) => _repo.DeleteLoan(p);


    }
}
