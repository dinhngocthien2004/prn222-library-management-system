using BusinessObjects.Entities;
using Microsoft.EntityFrameworkCore;
using Repositories;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Services
{
    public class LoanService : ILoanService
    {
        private readonly ILoanRepository _repo;
        public LoanService(ILoanRepository repo) => _repo = repo;

        // 🔥 FIX CHÍNH Ở ĐÂY
        public IEnumerable<Loan> GetLoans()
        {
            return _repo.GetLoans()
                .AsQueryable()
                .Include(l => l.Copy)
                    .ThenInclude(c => c.Book)
                        .ThenInclude(b => b.Category)
                .ToList();
        }

        public Loan? GetLoanById(int id) => _repo.GetLoanById(id);

        public void SaveLoan(Loan p) => _repo.SaveLoan(p);

        public void UpdateLoan(Loan p) => _repo.UpdateLoan(p);

        public void DeleteLoan(Loan p) => _repo.DeleteLoan(p);

        public List<Loan> GetHistory()
        {
            return _repo.GetHistory();
        }

        public void ReturnBook(int id)
        {
            var loan = _repo.GetLoanById(id);

            loan.IsReturned = true;
            loan.ReturnDate = DateTime.Now;

            _repo.UpdateLoan(loan);
        }

        public int GetBorrowedQuantity(int userId, int bookId)
        {
            return _repo.GetLoans()
                .Where(l => l.UserId == userId
                         && l.Copy.BookId == bookId
                         && l.IsReturned == false)
                .Count();
        }
    }
}