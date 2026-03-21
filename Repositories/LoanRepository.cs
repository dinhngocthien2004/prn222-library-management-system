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
        private readonly LibraryManagementDbContext _context;
        public LoanRepository(LoanDAO dao, LibraryManagementDbContext context)
        {
            _dao = dao;
            _context = context;
        }

        public IEnumerable<Loan> GetLoans()
        {
            return _context.Loans
                .Include(l => l.Copy)
                .ToList();
        }
        public Loan? GetLoanById(int id) => _dao.GetById(id);
        public void SaveLoan(Loan p) => _dao.Create(p);
        public void UpdateLoan(Loan p) => _dao.Update(p);
        public void DeleteLoan(Loan p) => _dao.Delete(p);
        public List<Loan> GetHistory()
        {
            return _context.Loans
                .Where(l => l.IsReturned == true)
                .ToList();
        }
    }
}
