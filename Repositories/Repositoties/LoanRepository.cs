using LibraryManagementSystem.DAL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibraryManagementSystem.DAL.Repositoties
{
    public class LoanRepository
    {
        private readonly LibraryManagementDbContext _context;

        public LoanRepository(LibraryManagementDbContext context)
        {
            _context = context;
        }

        public List<Loan> GetAll()
        {
            return _context.Loans.ToList();
        }

        public void Add(Loan loan)
        {
            _context.Loans.Add(loan);
            _context.SaveChanges();
        }

        public Loan GetById(int id)
        {
            return _context.Loans.FirstOrDefault(l => l.LoanId == id);
        }

        public void Update(Loan loan)
        {
            _context.Loans.Update(loan);
            _context.SaveChanges();
        }
    }
}
