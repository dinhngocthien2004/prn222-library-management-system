using BusinessObjects.Entities;
using DataAccessObjects;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repositories
{
    public class LoanRepository : ILoanRepository
    {
        private readonly LoanDAO _dao;
        public LoanRepository(LoanDAO dao) => _dao = dao;

        public IEnumerable<Loan> GetLoans() => _dao.GetAll();
        public Loan? GetLoanById(int id) => _dao.GetById(id);
        public void SaveLoan(Loan p) => _dao.Create(p);
        public void UpdateLoan(Loan p) => _dao.Update(p);
        public void DeleteLoan(Loan p) => _dao.Delete(p);
    }
}
