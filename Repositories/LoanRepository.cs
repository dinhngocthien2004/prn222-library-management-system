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
        private readonly LibraryManagementDbContext _context;

        public LoanRepository(LibraryManagementDbContext context)
        {
            _context = context;
        }

        public List<Loan> GetAll()
        {
            return _context.Loans
                           .Include(l => l.User)
                           .Include(l => l.Copy)
                           .ToList();
        }

        public Loan? GetById(int id)
        {
            return _context.Loans
                           .Include(l => l.User)
                           .Include(l => l.Copy)
                           .FirstOrDefault(l => l.LoanId == id);
        }

        public void Add(Loan loan)
        {
            _context.Loans.Add(loan);
        }

        public void Update(Loan loan)
        {
            _context.Loans.Update(loan);
        }

        public void Delete(int id)
        {
            var loan = _context.Loans.Find(id);
            if (loan != null)
            {
                _context.Loans.Remove(loan);
            }
        }

        public void Save()
        {
            _context.SaveChanges();
        }
    }
}
